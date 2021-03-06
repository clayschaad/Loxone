import React, { useState, useEffect } from 'react';
import { Button, ButtonGroup, Card, CardHeader, CardContent, Grid, Select, FormControl, IconButton } from '@material-ui/core'
import ArrowDropUpIcon from '@material-ui/icons/ArrowDropUp';
import ArrowDropDownIcon from '@material-ui/icons/ArrowDropDown';
import EmojiObjectsOutlinedIcon from '@material-ui/icons/EmojiObjectsOutlined';
import { makeStyles } from '@material-ui/core/styles'

const useStyles = makeStyles((theme) => ({
    card: {
        margin: 5,
        minWidth: 250,
        //display: "flex",
        flexDirection: "column",
        justifyContent: "space-between"
    }
}))

export default function Loxone() {

    const classes = useStyles();

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

    const onClickLightOut = (event) => {
        const data = { Id: event.currentTarget.id, SceneId: 2 };
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
            <Grid container>
                {loxoneRooms.rooms.map(room => {
                    var firstLightControl = room.lightControls.length > 0 ? room.lightControls[0] : null;
                    return (
                        <Grid item component={Card} key={room.id} className={classes.card}>
                            <CardHeader
                                title={room.name}
                                action={
                                    <IconButton color="secondary" id={firstLightControl.id} onClick={onClickLightOut}><EmojiObjectsOutlinedIcon /></IconButton>
                                }
                            />
                                           
                            <CardContent key={firstLightControl.id}>
                                <h5>{firstLightControl.name}</h5>
                                <FormControl>
                                    <Select native value={firstLightControl.selectedSceneId ?? 0} onChange={lightSceneChanged} inputProps={{ id: firstLightControl.id, name: firstLightControl.id }}>
                                        <option value={2}></option>
                                        {firstLightControl.lightScenes.map(scene => {
                                            return <option key={scene.id} value={scene.id}>{scene.name}</option>
                                        })}
                                    </Select>
                                </FormControl>
                            </CardContent>
                     

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
                    </Grid>
                )})}

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
