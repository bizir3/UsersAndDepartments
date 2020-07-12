import { message } from 'antd';

import axios from 'axios';

import { host } from '../utils';

const USERS = 'USERS';
const USER_ADD = 'USER_ADD';
const USERS_LOADING = 'USERS_LOADING';

const initialState = {
    users:[],
    users_loading:false
};
export const storeUsers = {
    getUsersByDepartment: (depId) => async (dispatch, getState) => {
        dispatch({ type: USERS_LOADING, payload: true });

        axios.get(host + "/users/getUsersByDepartment", {
            params: {
                depId
            }
        })
			.then((result) => {
				var data = result.data;
                dispatch({ type: USERS, payload: data });
                dispatch({ type: USERS_LOADING, payload: false });
			})
            .catch(err => dispatch({ type: USERS_LOADING, payload: false }));
        
    },
    addUser: (department) => async (dispatch, getState) => {
        dispatch({ type: USERS_LOADING, payload: true });

        axios.post(host + "/users/addUser", { ...department})
            .then((result) => {
                var data = result.data;
                dispatch({ type: USER_ADD, payload: data });
                dispatch({ type: USERS_LOADING, payload: false });
            })
            .catch(err => dispatch({ type: USERS_LOADING, payload: false }));

    }
};

export const reducer = (state = initialState, action) => {
    switch (action.type) {
        case USERS: return { ...state, users: [...action.payload] };
        case USER_ADD: return { ...state, users: [...state.users, action.payload] };
        case USERS_LOADING: return { ...state, users_loading: action.payload};

        default:
            return state;
    }
};
