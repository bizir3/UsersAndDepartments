import {combineReducers} from 'redux';

import * as Departments from '../store/Departments';
import * as Users from '../store/Users';


const allReducers = combineReducers ({
    users: Users.reducer,
    department: Departments.reducer
	
});

export default allReducers;
