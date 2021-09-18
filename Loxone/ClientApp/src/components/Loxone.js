import React, { Component } from 'react';
import { Button, ButtonGroup, Table } from 'reactstrap';

export class Loxone extends Component {
    static displayName = Loxone.name;

  constructor(props) {
    super(props);
      this.state = { loxoneRooms: null, loading: true };
      this.onClickJalousie = this.onClickJalousie.bind(this);
      this.onClickLight = this.onClickLight.bind(this);
  }

  componentDidMount() {
      this.getLoxoneRooms();
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
        const data = { Id: event.target.id, SceneId: event.target.name};
        fetch('loxoneroom/light', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    }

    renderLoxoneTable(loxoneRooms) {
    return (
        <Table dark bordered>
        <thead>
          <tr>
            <th>Room</th>
            <th>Controls</th>
          </tr>
        </thead>
        <tbody>
            {loxoneRooms.rooms.map(room =>
            <tr key={room.id}>
                <td>{room.name}</td>
                <td>
                    {room.lightControls.map(control => {
                        return <div key={control.id}>{control.name}
                            <ButtonGroup>
                            {control.lightScenes.map(scene => {
                                return <Button outline color="warning" id={control.id} name={scene.id} onClick={this.onClickLight}>{scene.name}</Button>
                            })}
                            </ButtonGroup>
                        </div>
                    })}

                    {room.jalousieControls.map(control => {
                        return <div key={control.id}>{control.name}
                            <ButtonGroup>
                                <Button outline color="success" id={control.id} name="up" onClick={this.onClickJalousie}>▲</Button>
                                <Button outline color="success" id={control.id} name="down" onClick={this.onClickJalousie}>▼</Button>
                            </ButtonGroup>
                        </div>
                    })}
                </td>
            </tr>
          )}
        </tbody>
      </Table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : this.renderLoxoneTable(this.state.loxoneRooms);

    return (
      <div>
        <h1 id="tabelLabel" >Loxone Config</h1>
        <p>Test mit Loxone</p>
        {contents}
      </div>
    );
  }

  async getLoxoneRooms() {
    const response = await fetch('loxoneroom');
      const data = await response.json();
    this.setState({ loxoneRooms: data, loading: false });
    }

}
