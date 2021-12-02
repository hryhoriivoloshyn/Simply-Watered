import React, { Component } from 'react';
import { Route, Switch} from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import { GroupList } from './components/RegionGroups';
import {GroupAdding} from './components/RegionGroupsAdding';
import {RegionList} from './components/Regions';
import {RegionAdding} from './components/RegionsAdding';
import { DeviceList } from './components/Devices';
import {ReadingList} from './components/Readings';
import {ScheduleList} from './components/ScheduleList';
import { ScheduleAdding } from './components/SchedulesAdding';
import './custom.css'
import { IrrigationHistory } from './components/IrrigationHistory';

export default class App extends Component {
  static displayName = App.name;
  render () {
    return (
      <Layout>
          <Switch>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <AuthorizeRoute path='/fetch-data' component={FetchData} /> 
            <AuthorizeRoute exact path='/regiongroups' component={GroupList} />
            <AuthorizeRoute path='/regiongroups/add' component={GroupAdding} />
            <AuthorizeRoute exact path='/regiongroups/:id/regions' exact component={RegionList} />
            <AuthorizeRoute exact path='/regiongroups/:id/regions/add' component={RegionAdding} />
            <AuthorizeRoute exact path='/regiongroups/:id/regions/:id/devices' component={DeviceList} />
            <AuthorizeRoute exact path='/regiongroups/:id/regions/:id/devices/:id/readings' component={ReadingList} />
            <AuthorizeRoute exact path='/regiongroups/:id/regions/:id/devices/:id/irrigationhistory' component={IrrigationHistory} />
            <AuthorizeRoute exact path='/regiongroups/:id/schedules' component={ScheduleList} />
            <AuthorizeRoute path='/regiongroups/:id/schedules/add' component={ScheduleAdding}/>
          </Switch> 
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
