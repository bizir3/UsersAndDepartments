import React, { Component } from 'react';

import { Route } from 'react-router';
import {Provider} from 'react-redux';

import { createStore , applyMiddleware } from "redux";
import allReducers from './store';

import { Layout } from './components/Layout';
import Home from './route/Home';

import {composeWithDevTools} from 'redux-devtools-extension';
import thunk from 'redux-thunk';

import 'antd/dist/antd.css';
import './custom.css'

const store = createStore(allReducers,composeWithDevTools(applyMiddleware(thunk)));

export default class App extends Component {
	static displayName = App.name;

	render () {
		return (
			<Layout>
				<Provider store={store}>
					<Route exact path='/' component={Home} />
				</Provider>
			</Layout>
		);
	}
}
