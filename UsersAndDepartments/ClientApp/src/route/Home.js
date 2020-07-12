import React, { Component } from 'react';
import './index.css';

import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import ContentOrEmpty from '../components/ContentOrEmpty';

import { Row, Col, Input, Form, Button } from 'antd';
import { LoadingOutlined, PlusOutlined } from '@ant-design/icons';

import { storeDepartments } from '../store/Departments';
import { storeUsers } from '../store/Users';

const { getDepartments, addDepartment, updateDepartment, deleteDepartment } = storeDepartments;
const { getUsersByDepartment, addUser } = storeUsers;

const tailLayout = {
	wrapperCol: { offset: 8, span: 16 },
};

class Home extends Component {
	formUpdate = React.createRef();

	state = {
		selectListId : 1,
		changeParams: {}
	}

	componentWillMount() {
		this.props.getDepartments();
	}

	uploadButtonUser(user) {
		return <div key={user.userId} className={"ant-upload ant-upload-select ant-upload-select-picture-card"}>
			<div className={"ant-upload"}>
				<div className="ant-upload-text">{user.fio}</div>
			</div>
		</div>
	}

	uploadButton(department) {
		const { getUsersByDepartment } = this.props;
		const { selectDepartmentId } = this.state;
		return <div key={department.departmentId} onClick={() => {
			this.setState({ selectDepartmentId: department.departmentId == selectDepartmentId ? null : department.departmentId });
			getUsersByDepartment(department.departmentId);
			setTimeout(() => {
				if (this.formUpdate.current) {
					this.formUpdate.current.setFieldsValue({
						...department
					});
				}
			});
		}} className={(selectDepartmentId == department.departmentId && "selected") + " " + "ant-upload ant-upload-select ant-upload-select-picture-card"}>
			<div className={"ant-upload"}>
				<div className="ant-upload-text">{department.name}</div>
			</div>
		</div>
	} 
	
	render() {

		const { departments_loading, departments, users, users_loading, addDepartment, updateDepartment, deleteDepartment, addUser } = this.props;
		const { selectDepartmentId } = this.state;

		var lists = [{ title:"123", id:1 }];
		return 	<Row>
					<ContentOrEmpty
						loading={departments_loading}
						isContent={true}
						emptyDescription={""}
						title={"Отделы"}
						render={() => (
							<Row>
								<Col xs={24}>
									{
										departments.map(d => this.uploadButton(d, false))
									}
								</Col>
								{
									selectDepartmentId == null &&
										<Col xs={24}>
											<Form
												name="basic"
												initialValues={{ remember: true }}
												onFinish={(val) => addDepartment(val)}
											>
												<Form.Item
													label="Название"
													name="Name"
													rules={[{ required: true, message: 'Введите название отдела!' }]}
												>
													<Input placeholder="Название отдела" />
												</Form.Item>

												<Form.Item>
													<Button type="primary" htmlType="submit">
														Добавить
													</Button>
												</Form.Item>
											</Form>
										</Col>
								}
								{
									selectDepartmentId &&
										<Col xs={24}>
											<Form
												ref={this.formUpdate}
												name="basic"
												initialValues={{ remember: true }}
												onFinish={(val) => updateDepartment({ departmentId: selectDepartmentId, ...val })}
											>
												<Form.Item
													label="Название"
													name="name"
													rules={[{ required: true, message: 'Введите название отдела!' }]}
												>
													<Input placeholder="Название отдела" />
												</Form.Item>
												<Form.Item>
													<Button type="primary" htmlType="submit">
														Изменить
													</Button>
												<Button type="danger" onClick={() => {
													deleteDepartment(selectDepartmentId);
													this.setState({ selectDepartmentId:null });
												}}>
														Удалить
													</Button>
												</Form.Item>
											</Form>
										</Col>
								}
							</Row>
						)}
					/>
					<ContentOrEmpty
						loading={users_loading}
						isContent={selectDepartmentId > 0}
						emptyDescription={"Не выбран отдел"}
						title={"Пользователи"}
						render={() => (
							<Row>
								<Col xs={24}>
									{
										users.map(d => this.uploadButtonUser(d))
									}
								</Col>
								<Col xs={24}>
									<Form
										name="basic"
										initialValues={{ remember: true }}
										onFinish={(val) => addUser({ ...val, depId: selectDepartmentId })}
									>
										<Form.Item
											label="ФИО"
											name="FIO"
											rules={[{ required: true, message: 'Введите ФИО пользователя!' }]}
										>
											<Input placeholder="ФИО пользователя" />
										</Form.Item>

										<Form.Item>
											<Button type="primary" htmlType="submit">
												Добавить
											</Button>
										</Form.Item>
									</Form>
								</Col>
							</Row>
						)}
					/>
				</Row>
	}
}

function mapStateToProps(state) {
	return {
		users: state.users.users,
		users_loading: state.users.users_loading,
		departments: state.department.departments,
		departments_loading: state.department.departments_loading,
	};
}
function matchDispatchToProps(dispatch) {
	return bindActionCreators({ getDepartments, addDepartment, updateDepartment, deleteDepartment, getUsersByDepartment, addUser }, dispatch);
}
export default connect(mapStateToProps, matchDispatchToProps)(Home);
