import React, { Component } from 'react';
import { Container } from '@material-ui/core'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <Container maxWidth="xl">
          {this.props.children}
        </Container>
      </div>
    );
  }
}
