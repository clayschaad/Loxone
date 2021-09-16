import React, { Component } from 'react';
import { Button } from 'reactstrap';

export class Loxone extends Component {
    static displayName = Loxone.name;

  constructor(props) {
    super(props);
      this.state = { loxoneData: [], loading: true };
      this.onClickJalousie = this.onClickJalousie.bind(this);
      this.onClickLight = this.onClickLight.bind(this);
  }

  componentDidMount() {
      this.getLoxoneData();
    }

    onClickJalousie(event) {
        const data = { Id: event.target.id, Direction: event.target.name};
        fetch('loxoneroom/jalousie', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    }

    onClickLight(event) {
        const data = { Id: event.target.id, Direction: event.target.name };
        fetch('loxoneroom/light', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    }

    renderLoxoneTable(loxoneData) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Controls</th>
          </tr>
        </thead>
        <tbody>
            {loxoneData.map(room =>
            <tr key={room.id}>
                <td>{room.name}</td>
                    <td>{room.controls.map(control =>
                        <div key={control.id}>{control.name}
                            <Button id={control.id} name="up" onClick={this.onClickJalousie}>Up</Button>
                            <Button id={control.id} name="down" onClick={this.onClickJalousie}>Down</Button>
                            <Button id={control.id} name="1" onClick={this.onClickLight}>Scene</Button>
                        </div>
                    )}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : this.renderLoxoneTable(this.state.loxoneData);

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
