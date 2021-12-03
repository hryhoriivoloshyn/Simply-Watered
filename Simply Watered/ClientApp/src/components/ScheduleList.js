import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'
import { GroupSettings } from './GroupSettings';

export class ScheduleList extends Component{
 
    constructor(props){
        super(props)
        this.state={
            schedules: [],
            regionGroup: {GroupName:"",
                          GroupDescription: ""
                         },
            // irrigationModes: [],
            minStartDate: "",    
            minEndDate: "",         
            path: this.props.location.pathname
        }
    }

    goBack =()=>{
        this.props.history.goBack();
    }

    onRemoveSchedule= async(schedule)=>{



    if (schedule) {

        let token = await authService.getAccessToken();

        let response = await fetch(`api${this.state.path}/${schedule.irrigScheduleId}`, {
            method: "DELETE",
            headers: !token ? {
                'Content-Type': 'application/json'
            } : {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
  
        }).then(async ()=>{
            await this.loadData();
        });

    }
    }
    componentDidMount() {
        this.loadData();
    }

    render(){
        //const state= this.props.location.state;
        let schedules=this.state.schedules;
        let groupName=this.state.regionGroup.groupName;
        return(
            <>
            
            <h2 className="text-center">Розклад зрошення ділянок групи "{groupName}"</h2>
            <hr />

            <Link
                className="btn btn-primary mx-3"
                role="button"
                to=
                {{
                pathname: `${this.state.path}/add`,
                state: {
                     minStartDate: this.state.minStartDate,
                     minEndDate: this.state.minEndDate
                    }
                }}
                >Додати розклад</Link>

            <button className="btn btn-secondary" onClick={this.goBack}>Повернутися</button>

            {/* <GroupSettings groupId={this.state.groupId} irrigationModes={this.state.irrigationModes} loadData={this.loadData.bind(this)} ></GroupSettings> */}

            <table className='table table-striped text-center mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Дата початку</th>
                        <th>Дата завершення</th>
                        <th>Час початку</th>
                        <th>Час завершення</th>
                        <th>Дії</th>
                    </tr>
                </thead>
                <tbody>
                {this.state.schedules!=null &&
                 schedules.map(schedule => <tr key={schedule.irrigScheduleId}>
                    <td>{schedule.irrigScheduleName}</td>
                    <td>{schedule.scheduleStartDate.substr(0,10)}</td>
                    <td>{schedule.scheduleEndDate.substr(0,10)}</td>
                    <td>{schedule.scheduleTimespans[0].start.substr(0,5)}</td>
                    <td>{schedule.scheduleTimespans[0].finish.substr(0,5)}</td>
                    <td><button className="btn btn-outline-dark" onClick={async () => { await this.onRemoveSchedule(schedule); } }>Видалити</button></td>
                    <td>
                    </td>
                </tr>
                )
                }   
                   
                </tbody>
            </table>
            
            </>
            );
    }

    async loadData() {
        const token = await authService.getAccessToken();
        console.log(token);

        const response = await fetch(`api${this.state.path}`, {
            method: "GET",
            headers: !token ? { 
                'Content-Type': 'application/json'
             } : {
                  'Content-Type': 'application/json',
                   'Authorization': `Bearer ${token}` 
                },

        });

        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ schedules: data.schedules,
             regionGroup:data.regionGroup,
            //  irrigationModes: data.irrigationModes,
             minStartDate:data.minStartDate,
             minEndDate: data.minEndDate,
             loading: false });
    }
}