import React, { Component } from 'react';
import { Redirect } from "react-router-dom";
import authService from './api-authorization/AuthorizeService'
import { useLocation } from 'react-router-dom'
import { Link } from 'react-router-dom'

export class ScheduleAdding extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirect: null,
            groupId: this.props.location.state.groupId,

            minStartDate: this.props.location.state.minStartDate,
            minEndDate: this.props.location.state.minEndDate,
            endDateDisabled: true,

            endTimeDisabled: true,
            minTime: '',

            fields: {},
            errors: {}
        };
    }

    goBack =()=>{
        this.props.history.goBack();
    }

    handleValidation = async () => {
        let fields = this.state.fields;
        let errors = {};
        let formIsValid = true;
    
        
        if (!fields["scheduleName"]) {
          formIsValid = false;
          errors["scheduleName"] = "Вкажіть назву для розкладу!";
        }

        if (!fields["scheduleStartDate"]) {
            formIsValid = false;
            errors["scheduleStartDate"] = "Вкажіть дату початку розкладу!";
          }

        if (!fields["scheduleEndDate"]) {
        formIsValid = false;
        errors["scheduleEndDate"] = "Вкажіть дату завершення розкладу!";
        }

        if (fields["scheduleEndDate"]===fields["scheduleStartDate"]&&fields["scheduleEndDate"]) {
            formIsValid = false;
            errors["scheduleEndDate"] = "Дата завершення не повинна співпадати з датою початку!";
            }

        if (!fields["timespanStart"]) {
        formIsValid = false;
        errors["timespanStart"] = "Вкажіть час початку зрошення!";
        }  

        if (!fields["timespanEnd"]) {
            formIsValid = false;
            errors["timespanEnd"] = "Вкажіть час завершення зрошення!";
            } 

        if (fields["timespanEnd"]===fields["timespanStart"]&&fields["timespanEnd"]) {
            formIsValid = false;
            errors["timespanEnd"] = "Час завершення не повинен співпадати з часом початку!";
            } 

      
    
        this.setState({ errors: errors });
        return formIsValid;
      }

    scheduleSubmit= async (e) => {
        
        e.preventDefault()
        if  (await this.handleValidation()) {
            await this.onCreateSchedule();
        } 
    }

    handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields: fields });
    }

    handleTimeChange(field, e){
        let fields = this.state.fields;
    fields[field] = e.target.value;
    if(e.target.value){
        this.setState({ fields: fields, endTimeDisabled: false, minTime:e.target.value });
    }else{
        this.setState({ fields: fields,endTimeDisabled: true  });
    }
    }

    handleDateChange(field, e){
        let fields = this.state.fields;
        fields[field] = e.target.value;
        if(e.target.value){
            this.setState({ fields: fields, endDateDisabled: false, minEndDate:e.target.value });
        }else{
            this.setState({ fields: fields,endDateDisabled: true  });
        }
    }

    onCreateSchedule = async () => {

        console.log("кнока нажата");
        let scheduleModel = {
            ScheduleName: this.state.fields["scheduleName"],
            ScheduleStartDate: this.state.fields["scheduleStartDate"],
            ScheduleEndDate: this.state.fields["scheduleEndDate"],
            Timespan: {
                Start: this.state.fields["timespanStart"],
                Finish: this.state.fields["timespanEnd"],
            },
            GroupId: this.state.groupId
        };
        console.log(scheduleModel);
        console.log("Добавление");
        let token = await authService.getAccessToken();
        console.log(token);
        await fetch('schedules/add', {
            method: "POST",
            headers: !token ? {
                'Content-Type': 'application/json'
            } : {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(scheduleModel)
        }).then(() => { this.setState({ redirect: "/schedules" }) });


    }

    render() {
        if (this.state.redirect) {
            return <Redirect  to=
                                {{
                                pathname: this.state.redirect,
                                state: { groupId: this.state.groupId }
                                }}/>
        }
        
        return (
            <>
            <form
                name="scheduleForm"
                className="scheduleForm mt-3"
                onSubmit={async(event)=> {
                    await this.scheduleSubmit(event);
                }}
                >      
                    <div className="col-md-6">
                    <h2>Введіть дані розкладу зрошення</h2>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.ScheduleName">Назва розкладу зрошення*</label>
                                    <input className="form-control valid"
                                        id="Input.ScheduleName"
                                        ref="scheduleName"
                                        type="text"
                                        size="30"
                                        placeholder="Назва"
                                        onChange={this.handleChange.bind(this, "scheduleName")}
                                        value={this.state.fields["scheduleName"]}
                                    />   
                                    <span className="error-message">{this.state.errors["scheduleName"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.ScheduleStartDate">Дата початку*</label>
                                    <input className="form-control valid"
                                        id="Input.ScheduleStartDate"
                                        ref="scheduleStartDate"
                                        type="date"
                                        min={this.state.minStartDate}
                                        onChange={this.handleDateChange.bind(this, "scheduleStartDate")}
                                        value={this.state.fields["scheduleStartDate"]}
                                    />   
                                    <span className="error-message">{this.state.errors["scheduleStartDate"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.ScheduleEndDate">Дата завершення*</label>
                                    <input className="form-control valid"
                                        disabled={this.state.endDateDisabled}
                                        id="Input.ScheduleEndDate"
                                        ref="scheduleEndDate"
                                        type="date"
                                        min={this.state.minEndDate}
                                        onChange={this.handleChange.bind(this, "scheduleEndDate")}
                                        value={this.state.fields["scheduleEndDate"]}
                                    />   
                                    <span className="error-message">{this.state.errors["scheduleEndDate"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.TimespanStart">Час початку*</label>
                                    <input className="form-control valid"
                                        id="Input.TimespanStart"
                                        ref="timespanStart"
                                        type="time"
                                        onChange={this.handleTimeChange.bind(this, "timespanStart")}
                                        value={this.state.fields["timespanStart"]}
                                    />   
                                    <span className="error-message">{this.state.errors["timespanStart"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.TimespanEnd">Час завершення*</label>
                                    <input className="form-control valid"
                                        disabled={this.state.endTimeDisabled}
                                        id="Input.TimespanEnd"
                                        ref="timespanEnd"
                                        type="time"
                                        min={this.state.minTime}
                                        onChange={this.handleChange.bind(this, "timespanEnd")}
                                        value={this.state.fields["timespanEnd"]}
                                    />   
                                    <span className="error-message">{this.state.errors["timespanEnd"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group ">
                                <input type="submit" value="Додати розклад" class="btn btn-primary"></input>
                            </div>
                    </div>
            </form>

            <div className="col-md-6">
            <button className="btn btn-secondary" onClick={this.goBack}>Повернутися</button>
            </div>

    
    </>
)
}
}