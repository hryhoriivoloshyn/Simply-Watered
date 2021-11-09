import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'



export class RegionGroups extends Component {
    static displayName = RegionGroups.name;

    constructor(props) {
        super(props);
        this.state = { data: props.regiongroup }
        this.onClick = this.onClick.bind(this);
    }


    onClick(e) {
        this.props.onRemove(this.state.data);
    }

    render() {
        return (
            <tr>
            <td>{this.state.data.groupName}</td>
            <td>{this.state.data.regionGroupDescription}</td>
            <td><button onClick={this.onClick}>Видалити</button></td>
            </tr>
    );

}
}


export class GroupList extends Component {
    static displayName = GroupList.name;

    constructor(props) {
        super(props);
        this.state = {
            regiongroups: [],
            loading: true,
            message: ''
        }

        //this.onAddGroup = this.onAddGroup.bind(this);
        this.onRemoveGroup = this.onRemoveGroup.bind(this);
    }

    componentDidMount() {
        this.loadData();
    }

    static renderRegionGroupsTable(regiongroups){
       
            let remove = this.onRemoveGroup;
            return (
                <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr> 
                <th>Назва</th>
                <th>Опис</th>
                </tr>
                </thead>
                <tbody>
                {/*<GroupForm onGroupSubmit={this.onAddGroup} />*/}
                     
                        {regiongroups.map(regiongroup =>
                            <RegionGroups key={regiongroup.id} regiongroup={regiongroup} onRemove={remove} />
                           
                        
                    )}
                </tbody>
                </table>
                );
        }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : GroupList.renderRegionGroupsTable(this.state.regiongroups);

        return (
            <div>
            <h1 id="tabelLabel" >Region groups table</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
            </div>
            );
    }

    //Загрузка данных
    async loadData() {
        const token = await authService.getAccessToken();
        console.log(token);
        const response = await fetch('regiongroups', {
            headers: !token ? { 'Content-Type': 'application/json' } : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
        });
        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ regiongroups: data, loading: false });
    }

// Удаление группы
    async onRemoveGroup(regiongroup) {
        console.log("Проверка группы для удаления")
        if (regiongroup) {
            console.log("Удаление")
            const token = await authService.getAccessToken();
            console.log(token);
            const response = await fetch('regiongroups/Delete', {
                method: "POST",
                headers: !token ? {
                        'Content-Type': 'application/json'
                } : {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                },
                body: regiongroup.regionGroupId
                }).then(r=>r.json()).then(res => {
                if (res) {
                    this.setState({message: 'Группа удалена успешно'})
                }
                });
            console.log(response);
           

        }
    }

//onAddGroup(regionGroup) {
//    if (regionGroup) {
//        const data = new FormData();
//        data.append("name", regionGroup.name);
//        var xhr = new XMLHttpRequest();

//        xhr.open("post", this.props.ApiAuthorizationClientConfigurationUrl, true);
//        xhr.onload = function () {
//            if (xhr.status === 200) {
//                this.loadData();
//            }
//        }.bind(this);
//        xhr.send(data);
//    }
//}
}



//export class GroupForm extends Component {
//    constructor(props) {
//        super(props);
//        this.state = { name: ""};
//        this.onSubmit = this.onSubmit.bind(this);
//        this.onNameChange = this.onNameChange(this);
//    }

//    onNameChange(e) {
//        this.setState({ name: e.target.value });
//    }

//    onSubmit(e) {
//        e.preventDefault();
//        var groupName = this.state.name.trim();
//        if (!groupName) {
//            return;
//        }
//        this.props.onGroupSubmit({ name: groupName });
//        this.setState({ name: ""});
//    }
//    render() {
//        return (
//            <form onSubmit={this.onSubmit}>
//                <p>
//                    <input type="text"
//                        placeholder="Назва групи областей"
//                        value={this.state.name}
//                        onChange={this.onNameChange}/>
//                </p>
//                <input type="submit" value="Зберегти" />
//            </form>
//            );
//    }
//}


