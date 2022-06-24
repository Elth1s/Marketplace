import { Box, Button, Typography, IconButton, Grid, ListItem, ListItemButton, ListItemIcon, ListItemText } from '@mui/material'
import { ArrowBackIosNewOutlined, ArrowForwardIosOutlined, SportsEsportsOutlined } from '@mui/icons-material'
import { useState } from 'react'
import { Link } from 'react-router-dom';
import { ListItemButtonStyle, ButtonNoveltyStyle } from './styled';
import { gamepad } from '../../assets/icons';
import { homepage } from '../../assets/backgrounds';


const HomePage = () => {
    const [sidebarItems, setSidebarItems] = useState([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]);
    return (
        <Box sx={{ marginTop: "97px" }}>
            <Grid container>
                <Grid item xl={4}>
                    {sidebarItems.map((item, index) => (
                        <ListItem key={index} component={Link} to={"/"} disablePadding style={{ height: "51px", textDecoration: 'none', color: 'unset' }} >
                            <ListItemButtonStyle>
                                <ListItemIcon sx={{ paddingRight: "24px" }}>
                                    <img
                                        style={{ width: "25px", height: "20px" }}
                                        src={gamepad}
                                        alt="icon"
                                    />
                                </ListItemIcon>
                                <Typography color="#000" fontSize="24px" sx={{ height: "auto" }}>Ноутбуки та комп'ютери</Typography>
                            </ListItemButtonStyle>
                        </ListItem>
                    ))}
                </Grid>
                <Grid item xl={8}>
                    <img
                        style={{ width: "100%" }}
                        src={homepage}
                        alt="background"
                    />
                </Grid>
            </Grid>
            <Box id="NewProducts" sx={{ marginTop: "100px", marginBottom: "190px" }}>
                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                    <Typography fontSize="27px">Novelty</Typography>
                    <ButtonNoveltyStyle variant="outlined" endIcon={<ArrowForwardIosOutlined />} >All novelties</ButtonNoveltyStyle>
                </Box>
                <Box sx={{ width: "100%", height: "415px", marginTop: "49px", display: "flex", justifyContent: "space-between", alignContent: "center" }}>
                    <IconButton
                        sx={{ color: "#7e7e7e", borderRadius: '12px', p: 0, "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                        size="large"
                        aria-label="search"

                    >
                        <ArrowBackIosNewOutlined sx={{ fontSize: "35px" }} />
                    </IconButton>
                    <Box sx={{ width: "305px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "5px" }}></Box>
                    <Box sx={{ width: "305px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "5px" }}></Box>
                    <Box sx={{ width: "305px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "5px" }}></Box>
                    <Box sx={{ width: "305px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "5px" }}></Box>
                    <Box sx={{ width: "305px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "5px" }}></Box>
                    <IconButton
                        sx={{ color: "#7e7e7e", borderRadius: '12px', p: 0, "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                        size="large"
                        aria-label="search"
                    >
                        <ArrowForwardIosOutlined sx={{ fontSize: "35px" }} />
                    </IconButton>
                </Box>
            </Box>
        </Box>
    )
}

export default HomePage