import { useLocation } from 'react-router-dom'
import { useParams } from 'react-router-dom';
import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'
import { GroupSettings } from './GroupSettings';

export class RegionList extends Component{
 
    constructor(props){
        super(props)
        
    
        this.state={
            regions: [],
            regionGroup: {groupName:" ",
                          groupDescription: " "
                         },
            modes: [],
            pathname: this.props.location.pathname,
            // groupId: this.state.pathname.substring(this.state.pathname.lastIndexOf('/')+1)
        }
    }

    goBack =()=>{
        this.props.history.goBack();
    }

    onRemoveRegion= async(region)=>{

        console.log("Проверка группы для удаления");
        let regionId= region.regionId;
    if (region) {
        console.log("Удаление");
        let token = await authService.getAccessToken();
        console.log(token);
        let response = await fetch(`api${this.state.pathname}/${regionId}`, {
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
        console.log(response);
    }
    }
    componentDidMount() {
        this.loadData();
    }

    render(){
        
   
        let regions=this.state.regions;
        let groupName=this.state.regionGroup.groupName;
        return(
            <>

            <h2 className="text-center">Ділянки групи "{groupName}"</h2>
            <hr />

           
            {this.state.modes.length!=0 &&
            <GroupSettings groupId={this.state.groupId} pathname={this.state.pathname} modes={this.state.modes} loadData={this.loadData.bind(this)} ></GroupSettings>
            }


            <Link
                className="btn btn-primary mx-3"
                role="button"
                to=
                {{
                pathname: `${this.state.pathname}/add`,
               
                }}
                >Додати ділянку</Link>
            <button className="btn btn-secondary ml-3" onClick={this.goBack}>Повернутися</button>

            <table className='table table-striped text-center mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Опис</th>


                        <th colSpan="2">Дії</th>
                    </tr>
                </thead>
                <tbody>

                
                    
                        
                    {regions.map(region => <tr key={region.regionId}>
                        <td>{region.regionName}</td>
                        <td>{region.regionDescription}</td>
                        
                        <tr>
                        <th>Назва пристрою</th>
                        <th>Режим роботи</th>
                        <th>Мінімальна вологість</th>
                        <th>Максимальна вологість</th>
                        </tr>
                      
                       {region.devices.map(device=>
                        <tr className="table-info" key={device.deviceId}>
                            <td>{device.deviceType.deviceName}</td>
                            <td>{device.irrigMode.modeName}</td>
                            <td>{device.minimalHumidity}%</td>
                            <td>{device.maxHumidity}%</td>
                        </tr>
                            
                            )}
                      
                       
                            
                        <td>
                        <Link
                            className="btn btn-outline-primary my-auto"
                            role="button"
                            to=
                                {{
                                pathname: `${this.state.pathname}/${region.regionId}/devices`,
                               
                                }}

                            >Переглянути пристрої</Link>
                        </td>
                        <td>
                            <button className="btn btn-outline-dark" onClick={async () => { await this.onRemoveRegion(region); } }>
                            Видалити
                            </button>
                       </td>
                    </tr>


                    )}
                </tbody>
            </table>
            
            </>
            );
    }

    async loadData() {
        const token = await authService.getAccessToken();
     
        
        console.log(token);
        const response = await fetch(`api${this.state.pathname}`, {
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
        this.setState({ regions: data.regions, modes: data.modes, regionGroup:data.regionGroup, loading: false });
    }
}