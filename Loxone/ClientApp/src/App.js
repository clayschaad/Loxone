import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Loxone from './components/Loxone';

import './custom.css'
import { ThemeProvider } from '@material-ui/core';
import { createTheme } from '@material-ui/core/styles';

// https://bareynol.github.io/mui-theme-creator/#Accordion
const myDarkTheme = createTheme({
    palette: {
        type: 'dark'
    },
});

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
          <ThemeProvider theme={myDarkTheme}>
          <Layout>
            <Route exact path='/' component={Loxone} />
          </Layout>
        </ThemeProvider>
    );
  }
}
