import { message } from 'antd';

import axios from 'axios';

import { host } from '../utils';
import { setInterval } from 'core-js';

const DEPARTMENTS = 'DEPARTMENTS';
const DEPARTMENT_ADD = 'DEPARTMENT_ADD';
const DEPARTMENT_UPDATE = 'DEPARTMENT_UPDATE';
const DEPARTMENT_DELETE = 'DEPARTMENT_DELETE';
const DEPARTMENTS_LOADING = 'DEPARTMENTS_LOADING';

const initialState = {
    departments:[],
    departments_loading:false
};
export const storeDepartments = {
    getDepartments: () => async (dispatch, getState) => {
        dispatch({ type: DEPARTMENTS_LOADING, payload: true });

		axios.get(host + "/departments/getDepartments", {})
			.then((result) => {
				var data = result.data;
				dispatch({ type: DEPARTMENTS, payload: data });
				dispatch({ type: DEPARTMENTS_LOADING, payload: false });
			})
		.catch(err => dispatch({ type: DEPARTMENTS_LOADING, payload: false }));
        
    },
    addDepartment: (department) => async (dispatch, getState) => {
        dispatch({ type: DEPARTMENTS_LOADING, payload: true });

        axios.post(host + "/departments/addDepartment", { ...department})
            .then((result) => {
                var data = result.data;
                dispatch({ type: DEPARTMENT_ADD, payload: data });
                dispatch({ type: DEPARTMENTS_LOADING, payload: false });
            })
            .catch(err => dispatch({ type: DEPARTMENTS_LOADING, payload: false }));

    },
    deleteDepartment: (departmentId) => async (dispatch, getState) => {
        dispatch({ type: DEPARTMENTS_LOADING, payload: true });
        axios.delete(host + "/departments/deleteDepartment", { data: { departmentId } })
            .then((result) => {
                var data = result.data;
                dispatch({ type: DEPARTMENT_DELETE, payload: departmentId });
                dispatch({ type: DEPARTMENTS_LOADING, payload: false });
            })
            .catch(err => dispatch({ type: DEPARTMENTS_LOADING, payload: false }));

    },
    updateDepartment: (department) => async (dispatch, getState) => {
        dispatch({ type: DEPARTMENTS_LOADING, payload: true });

        axios.put(host + "/departments/updateDepartment", { ...department })
            .then((result) => {
                var data = result.data;
                dispatch({ type: DEPARTMENT_UPDATE, payload: data });
                dispatch({ type: DEPARTMENTS_LOADING, payload: false });
            })
            .catch(err => dispatch({ type: DEPARTMENTS_LOADING, payload: false }));

    },
};

export const reducer = (state = initialState, action) => {
    switch (action.type) {
        case DEPARTMENTS: return { ...state, departments: [...action.payload] };
        case DEPARTMENT_ADD: return { ...state, departments: [...state.departments, action.payload] };
        case DEPARTMENT_DELETE: return { ...state, departments: state.departments.filter(d => d.departmentId != action.payload)};
        case DEPARTMENT_UPDATE: return { ...state, departments: state.departments.map(d => d.departmentId == action.payload.departmentId ? action.payload : d)};
        case DEPARTMENTS_LOADING: return { ...state, departments_loading: action.payload};

        default:
            return state;
    }
};
