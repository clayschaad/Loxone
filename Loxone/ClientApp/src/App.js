import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Loxone } from './components/Loxone';

import './custom.css'
import { createMuiTheme, ThemeProvider } from '@material-ui/core';

// https://bareynol.github.io/mui-theme-creator/#Accordion
const myDarkTheme = createMuiTheme({
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
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
          </Layout>
        </ThemeProvider>
    );
  }
}
