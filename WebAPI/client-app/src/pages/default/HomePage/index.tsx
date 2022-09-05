import {
    Box,
    Typography,
    IconButton,
    Grid,
    ListItem,
    ListItemIcon,
    Paper,
    useTheme,
} from '@mui/material'
import {
    ArrowBackIosNewOutlined,
    ArrowForwardIosOutlined,
    PhotoOutlined,
} from '@mui/icons-material'
import { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next';

import { ListItemButtonStyle, ButtonNoveltyStyle, BoxProductOfTheDayStyle } from './styled';

import { gamepad, btn_arrow, arrow_right, arrow_left, deliver_car, shield, cash, arrow_return } from '../../../assets/icons';
import { homepage } from '../../../assets/backgrounds';

import LinkRouter from '../../../components/LinkRouter';

import { toast } from 'react-toastify';

import { ToastSuccess, ToastError, ToastWarning, ToastInfo } from '../../../components/ToastComponent';
import { useTypedSelector } from '../../../hooks/useTypedSelector';


const HomePage = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { fullCatalogItems } = useTypedSelector(state => state.catalog);

    const [selectedCategory, setSelectedCategory] = useState<number>(-1);
    const [allCategoriesMouseEnter, setAllCategoriesMouseEnter] = useState<boolean>(false);

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

    const isSelected = (index: number) => index == selectedCategory;

    return (
        <Box >
            <Grid container>
                <Grid item xl={3} onMouseOut={() => setSelectedCategory(-1)}>
                    {fullCatalogItems && fullCatalogItems.map((row, index) => {
                        const isItemSelected = isSelected(index);
                        if (index < 12)
                            return (
                                <LinkRouter
                                    key={`$catalog_${index}`}
                                    underline="none"
                                    color="inherit"
                                    to={`/catalog/${row.urlSlug}`}
                                >
                                    <Box sx={{ display: "flex", mb: "20px", alignItems: "center" }} >
                                        {row.lightIcon == "" || row.darkIcon == ""
                                            ? <PhotoOutlined color={isItemSelected ? "primary" : "inherit"} sx={{ fontSize: "20px" }} onMouseEnter={() => setSelectedCategory(index)} />
                                            : (palette.mode == "dark"
                                                ? <img
                                                    style={{ width: "20px", height: "20px", objectFit: "contain" }}
                                                    src={isItemSelected ? row.activeIcon : row.lightIcon}
                                                    alt="categoryIcon"
                                                    onMouseEnter={() => setSelectedCategory(index)}
                                                />
                                                : <img
                                                    style={{ width: "20px", height: "20px", objectFit: "contain" }}
                                                    src={isItemSelected ? row.activeIcon : row.darkIcon}
                                                    alt="categoryIcon"
                                                    onMouseEnter={() => setSelectedCategory(index)}
                                                />
                                            )
                                        }
                                        <Typography
                                            variant="h4"
                                            fontWeight="medium"
                                            color={isItemSelected ? "primary" : "inherit"}
                                            onMouseEnter={() => setSelectedCategory(index)}
                                            sx={{ pl: "10px" }}
                                        >
                                            {row.name}
                                        </Typography>
                                    </Box>
                                </LinkRouter>
                            );
                    })}
                    <LinkRouter underline="none" color="inherit" to={`/catalog`}>
                        <Typography
                            variant="h4"
                            color={allCategoriesMouseEnter ? "primary" : "inherit"}
                            sx={{ cursor: "pointer" }}
                            onMouseEnter={() => setAllCategoriesMouseEnter(true)}
                            onMouseOut={() => setAllCategoriesMouseEnter(false)}
                        >
                            {t("pages.home.allCategories")}
                        </Typography>
                    </LinkRouter>
                </Grid>
                <Grid item xl={9}>
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