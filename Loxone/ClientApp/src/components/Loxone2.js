import React, { Component } from 'react';
import { Button, ButtonGroup, CardColumns, Card, CardHeader, CardBody, CardSubtitle } from 'reactstrap';


export class Loxone2 extends Component {
    static displayName = Loxone2.name;

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
            <CardColumns>
                {loxoneRooms.rooms.map(room =>
                        <Card style={{ width: '35rem' }}>
                            <CardHeader tag="h5">{room.name}</CardHeader>

                            {room.lightControls.map(control => {
                                return (
                                    <CardBody>
                                        <CardSubtitle>{control.name}</CardSubtitle>
                                        <ButtonGroup>
                                            {control.lightScenes.map(scene => {
                                                return <Button outline color="warning" id={control.id} name={scene.id} onClick={this.onClickLight}>{scene.name}</Button>
                                            })}
                                        </ButtonGroup>
                                    </CardBody>);
                            })}

                            {room.jalousieControls.map(control => {
                                return (
                                    <CardBody>
                                        <CardSubtitle>{control.name}</CardSubtitle>
                                        <ButtonGroup>
                                            <Button outline color="success" id={control.id} name="up" onClick={this.onClickJalousie}>▲</Button>
                                            <Button outline color="success" id={control.id} name="down" onClick={this.onClickJalousie}>▼</Button>
                                        </ButtonGroup>
                                    </CardBody>);
                            })}
                        </Card>
                )}
            </CardColumns>
        );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : this.renderLoxoneTable(this.state.loxoneRooms);

    return (
      <div>
        <h1>Loxone Smarthome</h1>
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
