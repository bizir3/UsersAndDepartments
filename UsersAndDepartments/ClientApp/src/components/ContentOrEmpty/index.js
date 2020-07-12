import React from 'react';
import classnames from 'classnames';

import { Spin, Empty, Col } from 'antd';

const ContentOrEmpty = ({ title,render,emptyDescription, loading,isContent }) => {
    var content = <Empty
                        description={
                            emptyDescription
                        }
                    />;
    if (isContent) {
        content = render();
    }
    return (
                <Col
                    xs={24}
                    style={{
                        padding: "20px",
                        background: "white",
                        margin: "10px",
                    }}
                >
                    <Spin spinning={loading}>
                        <h2>{title}</h2>
                        {content}
                    </Spin>
                </Col>
           )
}
export default ContentOrEmpty;
