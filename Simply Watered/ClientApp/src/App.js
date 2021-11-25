import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import { RegionGroups } from './components/RegionGroups';
/*import { GroupForm } from './components/RegionGroups';*/
import { GroupList } from './components/RegionGroups';
import {GroupAdding} from './components/RegionGroupsAdding';
import {RegionList} from './components/Regions';
import {RegionAdding} from './components/RegionsAdding';
import { DeviceList } from './components/Devices';
import {ReadingList} from './components/Readings';
import { ScheduleList } from './components/Schedules';
import { ScheduleAdding } from './components/SchedulesAdding';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <AuthorizeRoute path='/fetch-data' component={FetchData} /> 
        <AuthorizeRoute path='/region-groups' component={GroupList} />
        <AuthorizeRoute path='/region-groups-add' component={GroupAdding} />
        <AuthorizeRoute path='/regions' component={RegionList} />
        <AuthorizeRoute path='/regions-add' component={RegionAdding} />
        <AuthorizeRoute path='/devices' component={DeviceList} />
        <AuthorizeRoute path='/readings' component={ReadingList} />
        <AuthorizeRoute path='/schedules' component={ScheduleList}/>
        <AuthorizeRoute path='/schedules-add' component={ScheduleAdding}/>

        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
