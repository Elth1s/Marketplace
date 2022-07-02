import {
    Box,
    Typography,
    IconButton,
    Grid,
    ListItem,
    ListItemIcon,
} from '@mui/material'
import {
    ArrowBackIosNewOutlined,
    ArrowForwardIosOutlined,
} from '@mui/icons-material'
import { useState } from 'react'

import { ListItemButtonStyle, ButtonNoveltyStyle, BoxProductOfTheDayStyle } from './styled';

import { gamepad } from '../../assets/icons';
import { homepage } from '../../assets/backgrounds';

import LinkRouter from '../../components/LinkRouter';

const HomePage = () => {
    const [sidebarItems, setSidebarItems] = useState([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]);
    return (
        <Box sx={{ marginTop: "97px" }}>
            <Grid container>
                <Grid item xl={4}>
                    {sidebarItems.map((item, index) => (
                        <LinkRouter key={index} underline="none" color="unset" to={"/"}>
                            <ListItem disablePadding style={{ height: "51px" }} >
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
                        </LinkRouter>
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
            <Box id="NewProducts" sx={{ marginTop: "100px" }}>
                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                    <Typography fontSize="27px">Novelty</Typography>
                    <ButtonNoveltyStyle variant="outlined" endIcon={<ArrowForwardIosOutlined />} >All novelties</ButtonNoveltyStyle>
                </Box>
                <Box sx={{ width: "100%", height: "366px", marginTop: "75px", display: "flex", justifyContent: "space-between", alignContent: "center" }}>
                    <IconButton
                        sx={{ color: "#7e7e7e", borderRadius: '12px', p: 0, "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                        size="large"
                        aria-label="search"

                    >
                        <ArrowBackIosNewOutlined sx={{ fontSize: "35px" }} />
                    </IconButton>
                    <Box sx={{ width: "269px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "9px" }}></Box>
                    <Box sx={{ width: "269px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "9px" }}></Box>
                    <Box sx={{ width: "269px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "9px" }}></Box>
                    <Box sx={{ width: "269px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "9px" }}></Box>
                    <Box sx={{ width: "269px", height: "100%", backgroundColor: "#0E7C3A", borderRadius: "9px" }}></Box>
                    <IconButton
                        sx={{ color: "#7e7e7e", borderRadius: '12px', p: 0, "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                        size="large"
                        aria-label="search"
                    >
                        <ArrowForwardIosOutlined sx={{ fontSize: "35px" }} />
                    </IconButton>
                </Box>
            </Box>
            <Grid container id="ProductOfTheDay" sx={{ height: "510px", marginTop: "190px", borderRadius: "9px", backgroundColor: "#0E7C3A" }}>
                <Grid item xl={3} sx={{ height: "100%" }}>
                    <Box sx={{ height: "100%", display: "flex", flexDirection: 'column', justifyContent: "center", alignItems: "center" }}>
                        <Typography color="#fff" fontSize="38px" sx={{ fontWeight: "500" }}>Product of the day</Typography>
                        <Typography color="#fff" fontSize="70px" sx={{ fontWeight: "700" }}>25.06</Typography>
                        <Typography color="#fff" fontSize="15px" sx={{ fontWeight: "500" }}>Limited quantity. The offer is valid only today</Typography>
                    </Box>
                </Grid>
                <Grid item xl={9} sx={{ display: "flex", justifyContent: "end", alignItems: "center" }}>
                    <BoxProductOfTheDayStyle>

                    </BoxProductOfTheDayStyle>
                </Grid>
            </Grid>
        </Box>
    )
}

export default HomePage