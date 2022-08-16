import {
    Box,
    Typography,
    IconButton,
    Grid,
    ListItem,
    ListItemIcon,
    Paper,
} from '@mui/material'
import {
    ArrowBackIosNewOutlined,
    ArrowForwardIosOutlined,
} from '@mui/icons-material'
import { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next';

import { ListItemButtonStyle, ButtonNoveltyStyle, BoxProductOfTheDayStyle } from './styled';

import { gamepad, btn_arrow, arrow_right, arrow_left, deliver_car, shield, cash, arrow_return } from '../../../assets/icons';
import { homepage } from '../../../assets/backgrounds';

import LinkRouter from '../../../components/LinkRouter';

import { toast } from 'react-toastify';

import { ToastSuccess, ToastError, ToastWarning, ToastInfo } from '../../../components/ToastComponent';

const HomePage = () => {
    const [sidebarItems, setSidebarItems] = useState([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]);
    const { t } = useTranslation();

    const featuresData = [
        {
            title: `${t('pages.home.features.deliver.title')}`,
            description: `${t('pages.home.features.deliver.description')}`,
            icon: deliver_car,
        },
        {
            title: `${t('pages.home.features.guarantee.title')}`,
            description: `${t('pages.home.features.guarantee.description')}`,
            icon: shield,
        },
        {
            title: `${t('pages.home.features.payment.title')}`,
            description: `${t('pages.home.features.payment.description')}`,
            icon: cash,
        },
        {
            title: `${t('pages.home.features.return.title')}`,
            description: `${t('pages.home.features.return.description')}`,
            icon: arrow_return,
        },
    ]

    useEffect(() => {
        document.title = "Mall";
        // toast.success(<ToastSuccess title="Success" message="Create complete" />);
        // toast.error(<ToastError title="Error" message="Somethings went wrong" />);
        // toast.warning(<ToastWarning title="Warning" message="Select category" />);
        // toast.info(<ToastInfo title="Info" message="Auth complele" />);
    }, [])

    return (
        <Box >
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
                    <Typography fontSize="27px">{t('pages.home.novelty.title')}</Typography>
                    <ButtonNoveltyStyle variant="outlined"
                        endIcon={<img
                            style={{ width: "16px", height: "16px" }}
                            src={btn_arrow}
                            alt="icon"
                        />}
                    >
                        {t('pages.home.novelty.button')}
                    </ButtonNoveltyStyle>
                </Box>
                <Box sx={{ width: "100%", height: "366px", marginTop: "75px", display: "flex", justifyContent: "space-between", alignContent: "center" }}>
                    <IconButton
                        sx={{ color: "#7e7e7e", borderRadius: '12px', p: 0, "&:hover": { background: "transparent" }, "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }}
                        size="large"
                        aria-label="search"
                    >
                        <img
                            style={{ width: "35px", height: "35px" }}
                            src={arrow_left}
                            alt="icon"
                        />
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
                        <img
                            style={{ width: "35px", height: "35px" }}
                            src={arrow_right}
                            alt="icon"
                        />
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
            <Box
                sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    mt: "160px"
                }}
            >
                {featuresData.map((item, index) => (
                    <Paper
                        key={index}
                        sx={{
                            display: "flex",
                            alignItems: "center",
                            width: "355px",
                            padding: "30px 28px",
                            border: "1px solid #7E7E7E",
                            borderRadius: "7px",
                        }}>
                        <img src={item.icon} alt="icon" />
                        <Box sx={{ ml: "28px" }}>
                            <Typography variant="h5" color="primary">{item.title}</Typography>
                            <Typography variant="h6" sx={{ mt: "16px" }}>{item.description}</Typography>
                        </Box>
                    </Paper>
                ))}
            </Box>

        </Box>
    )
}

export default HomePage