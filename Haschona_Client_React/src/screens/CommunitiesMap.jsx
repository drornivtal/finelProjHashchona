
import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";

import { CommunityContext } from "../contexts/CommunityContextProvider";

import { Box, Button, Container, TextField } from '@mui/material';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';

import LogoImage from '../assets/Logo_TheStreets.png';
import '../styles/CommunitesMapStyles.css';
import '../styles/LoginStyles.css';


// const communities2 = [
//     { id: 1, name: 'אשדוד', position: [31.801447, 34.643497] },
//     { id: 2, name: 'קרית טבעון', position: [32.722735, 35.127971] },
//     { id: 3, name: 'רמת ישי', position: [32.706755, 35.179418] },
//     { id: 4, name: 'טבעון-גולומב', position: [32.7049803, 35.1223932] },
// ];

export default function CommunitiesMap() {

    const { communities } = useContext(CommunityContext);
    const [searchTerm, setSearchTerm] = useState('');
    const navigate = useNavigate();

    const filteredCommunities = communities.filter((community) =>
        community.name.includes(searchTerm)
    );

    let regularRegisterObj = {
        isManager: false,
        // communityId: 4,
        inpCssClass: 'inpYellow',
        selectCssClass: 'selectYellow',

        btnCssId: 'btnToRegYellow',
        inpAboutCssId: 'inpAboutYellow',
        btnText: 'הרשמה'
    };
    let managerRegisterObj = {
        isManager: true,
        inpCssClass: 'inpPurple',
        selectCssClass: 'selectPurple',

        btnCssId: 'btnToNextLevelPurple',
        inpAboutCssId: 'inpAboutPurple',
        btnText: 'לשלב הבא'
    };
    const selectExistCommunity = (communityId) => {
        navigate('/Register', { state: { ...regularRegisterObj, communityId: communityId } });
    }

    return (
        <Container
            sx={{ height: '90vh', width: '80vw', display: 'flex', flexDirection: 'column' }}>

            {/* Logo Img Box */}
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'flex-start',
                    alignItems: 'center',
                    height: '20vh',
                    width: '100%'
                }}
            >
                <img
                    src={LogoImage}
                    alt="Logo"
                    style={{
                        maxWidth: '100%',
                        maxHeight: '100%',
                    }}
                />
            </Box>

            {/* searchTerm Box */}
            <Box
                sx={{ height: '7vh', width: '100%', display: 'flex', flexDirection: 'column', alignItems: 'center' }}
            >
                <TextField
                    // label="מצא קהילה באזור"
                    // variant="outlined" 
                    variant="standard"
                    placeholder="מצא קהילה באזור"
                    value={searchTerm}
                    color="secondary"
                    onChange={(e) => setSearchTerm(e.target.value)}
                    sx={{ direction: 'rtl', }}
                // className="inpPurple"
                />
            </Box>

            {/* map display Box */}
            <Box sx={{ height: '55vh', width: '100%', display: 'flex', flexDirection: 'column', alignItems: 'center', marginBottom: '2vh', }}>
                <MapContainer center={[31.801447, 34.643497]} zoom={7} style={{ height: '100%', width: '100%' }}>
                    <TileLayer
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    />
                    {filteredCommunities.map((community) => (
                        <Marker
                            key={community.communityId}
                            position={
                                [community.coordinates.latitude, community.coordinates.longitude]}
                        >
                            <Popup >
                                <Box className="PopUpOnMap">
                                    {community.name} <br />
                                    <Button
                                        // color='secondary'
                                        sx={{
                                            border: '1px solid black', color: "black",
                                            borderRadius: 10,
                                            marginTop: '10%',
                                            height: '4vh',
                                        }}
                                        onClick={() => selectExistCommunity(community.communityId)}
                                    >בחירת קהילה</Button>
                                </Box>
                            </Popup>
                        </Marker>
                    ))}
                </MapContainer>
            </Box>

            {/* newCommunity btn Box */}
            <Box
                sx={{ height: '4vh', width: '100%', display: 'flex', flexDirection: 'column', alignItems: 'center', marginBottom: '2vh' }}
            >
                <Button
                    variant='contained'
                    type='button'
                    id='btnNewCommunity'
                    onClick={() => (navigate('/Register', { state: managerRegisterObj }))}
                >
                    אין קהילה באזורך?
                    לפתיחת קהילה חדשה
                </Button>
            </Box>
        </Container>
    );
};

