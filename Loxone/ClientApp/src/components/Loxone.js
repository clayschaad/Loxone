import React, { Component } from 'react';
import { Button, ButtonGroup, Card, CardHeader, CardContent, Grid } from '@material-ui/core'

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
        const data = { Id: event.currentTarget.id, Direction: event.currentTarget.name};
        fetch('loxoneroom/jalousie', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    }

    onClickLight(event) {
        const data = { Id: event.currentTarget.id, SceneId: event.currentTarget.name};
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
            <Grid container spacing={3} direction="row">
                
                {loxoneRooms.rooms.map(room =>
                    <Grid item xs={6}>
                    <Card>
                        <CardHeader title={room.name} />
                        
                            {room.lightControls.map(control => {
                                return (
                                    <CardContent key={control.id}>
                                        <h5>{control.name}</h5>
                                        <ButtonGroup>
                                            {control.lightScenes.map(scene => {
                                                return <Button id={control.id} name={scene.id} key={scene.id} onClick={this.onClickLight}>{scene.name}</Button>
                                            })}
                                        </ButtonGroup>
                                    </CardContent>);
                            })}

                            {room.jalousieControls.map(control => {
                                return (
                                    <CardContent key={control.id}>
                                        <h5>{control.name}</h5>
                                        <ButtonGroup>
                                            <Button id={control.id} name="up" onClick={this.onClickJalousie}>▲</Button>
                                            <Button id={control.id} name="down" onClick={this.onClickJalousie}>▼</Button>
                                        </ButtonGroup>
                                    </CardContent>);
                            })}
                        </Card>
                    </Grid>
                    )}
                    
            </Grid>
        );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : this.renderLoxoneTable(this.state.loxoneRooms);

    return (
      <div>
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
