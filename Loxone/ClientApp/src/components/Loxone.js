import React, { Component } from 'react';

export class Loxone extends Component {
    static displayName = Loxone.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
      this.getLoxoneData();
  }

    static renderLoxoneTable(loxoneData) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
            {loxoneData.map(room =>
            <tr key={room.id}>
                <td>{room.id}</td>
                <td>{room.name}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : Loxone.renderLoxoneTable(this.state.loxoneData);

    return (
      <div>
        <h1 id="tabelLabel" >Loxone Config</h1>
        <p>Test mit Loxone</p>
        {contents}
      </div>
    );
  }

  async getLoxoneData() {
    const response = await fetch('loxoneroom');
    const data = await response.json();
    this.setState({ loxoneData: data, loading: false });
  }
}
