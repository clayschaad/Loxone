import React, { useState, useEffect } from 'react';
import { Button, ButtonGroup, Card, CardHeader, CardContent, Grid, Select, FormControl } from '@material-ui/core'
import ArrowDropUpIcon from '@material-ui/icons/ArrowDropUp';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';

export default function Loxone() {

    const [state, setState] = useState(
        {
            loxoneRooms: [],
            loading: true
        }
    );

    useEffect(() => {
        async function getLoxoneRooms() {
            const response = await fetch('loxoneroom');
            const data = await response.json();
            setState({
                ...state,
                loxoneRooms: data,
                loading: false
            });
        }

        getLoxoneRooms().then();
    }, []) // eslint-disable-line react-hooks/exhaustive-deps

    const onClickJalousie = (event) => {
        const data = { Id: event.currentTarget.id, Direction: event.currentTarget.name };
        fetch('loxoneroom/jalousie', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    }

    const lightSceneChanged = (event) => {
        const data = { Id: event.target.id, SceneId: event.target.value };
        fetch('loxoneroom/light', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        });
    };

    function renderLoxoneTable (loxoneRooms) {
        return (
            <Grid container spacing={3} direction="row">

                {loxoneRooms.rooms.map(room =>
                    <Grid key={room.id} item xs={12} md={3} xl={3}>
                        <Card variant="outlined">
                            <CardHeader title={room.name} />
                            {room.lightControls.map(control => {
                                
                                return (
                                    <CardContent key={control.id}>
                                        <h5>{control.name}</h5>
                                        <FormControl>
                                            <Select native value={control.selectedSceneId ?? 0} onChange={lightSceneChanged} inputProps={{ id: control.id, name: control.id }}>
                                                <option value={0}>-</option>
                                                {control.lightScenes.map(scene => {
                                                    return <option key={scene.id} value={scene.id}>{scene.name}</option>
                                                })}
                                            </Select>
                                            </FormControl>
                                    </CardContent>);
                            })}

                            {room.jalousieControls.map(control => {
                                return (
                                    <CardContent key={control.id}>
                                        <h5>{control.name}</h5>
                                        <ButtonGroup>
                                            <Button color="primary" variant="contained" id={control.id} name="up" onClick={onClickJalousie}><ArrowDropUpIcon /></Button>
                                            <Button color="primary" variant="contained" id={control.id} name="down" onClick={onClickJalousie}><ArrowDropDownIcon /></Button>
                                        </ButtonGroup>
                                    </CardContent>);
                            })}
                        </Card>
                    </Grid>
                )}

            </Grid>
        );
    }

    let contents = state.loading
        ? <p><em>Loading...</em></p>
        : renderLoxoneTable(state.loxoneRooms);

    return (
        <div>
            {contents}
        </div>
    );
}
