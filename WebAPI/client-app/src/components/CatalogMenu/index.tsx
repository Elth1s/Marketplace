import {
    alpha,
    Box,
    Button,
    Divider,
    Grid,
    Menu,
    Paper,
    Typography,
    useTheme
} from "@mui/material";
import { PhotoOutlined } from "@mui/icons-material"
import { useEffect, useState } from "react";

import { useActions } from "../../hooks/useActions";
import { useTypedSelector } from "../../hooks/useTypedSelector";

import { list, white_close } from "../../assets/icons";
import LinkRouter from "../LinkRouter";
import { useTranslation } from "react-i18next";

const CatalogMenu = () => {
    const { t } = useTranslation()
    const { palette } = useTheme();

    const { GetFullCatalog, UpdateSearch } = useActions();
    const { fullCatalogItems } = useTypedSelector(state => state.catalog);

    const [anchorEl, setAnchorEl] = useState(null);
    const open = Boolean(anchorEl);

    const [selectedCategory, setSelectedCategory] = useState<number>(0);

    useEffect(() => {
        getData();
    }, []);

    useEffect(() => {

    }, [selectedCategory]);

    const getData = async () => {
        try {
            await GetFullCatalog();
        } catch (ex) {
        }
    };

    const handleClick = (event: any) => {
        if (anchorEl == null)
            setAnchorEl(event.currentTarget);
        else
            setAnchorEl(null);
    };

    const handleClose = () => {
        UpdateSearch("")
        setAnchorEl(null);
    };

    const changeParentCategory = (index: number) => {
        setSelectedCategory(index)
    }

    const isSelected = (index: number) => index == selectedCategory;

    return (
        <>
            <Button variant="contained"
                onClick={handleClick}
                sx={{
                    width: "150px",
                    height: "50px",
                    fontSize: "18px",
                    lineHeight: "23px",
                    px: "21px",
                    textTransform: "none",
                    borderRadius: "10px",
                    zIndex: 1299,
                    "&>*:nth-of-type(1)": {
                        marginRight: "15px",
                        marginLeft: "0px"
                    },
                    "&:hover": { background: palette.primary.main }
                }}
                startIcon={
                    <img
                        style={{ width: "20px", height: "20px" }}
                        src={open ? white_close : list}
                        alt="icon"
                    />
                }
            >
                {t('containers.default.header.catalog')}
            </Button>
            <Menu
                anchorEl={anchorEl}
                id="catalog-menu"
                open={open}
                onClose={handleClose}
                sx={{
                    background: alpha("#777777", 0.4),
                    zIndex: 1298,
                }}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        borderRadius: "10px",
                        overflow: 'visible',
                        filter: 'drop-shadow(0 4px 8px rgba(0,0,0,0.5))',
                        mt: "135px",
                        mx: "auto",
                        minWidth: "1560px",
                        maxWidth: "1560px",
                        minHeight: "670px",
                        maxHeight: "670px",
                        position: "static",
                    },
                }}
                MenuListProps={{
                    sx: {
                        padding: 0,
                    },
                }}
            >
                <Grid container>
                    <Grid item xs={3}>
                        <Paper elevation={0}
                            sx={{
                                height: "622px",
                                px: "20px",
                                my: "24px",
                                overflow: 'auto',
                                '&::-webkit-scrollbar': { display: "none" }
                            }}
                        >
                            {fullCatalogItems && fullCatalogItems.map((row, index) => {
                                const isItemSelected = isSelected(index);

                                return (
                                    <LinkRouter key={`$catalog_${index}`} underline="none" color="inherit" to={`/catalog/${row.urlSlug}`} onClick={handleClose}>
                                        <Box sx={{ display: "flex", mb: "14px", alignItems: "center" }} onMouseEnter={() => changeParentCategory(index)}>
                                            {row.lightIcon == "" || row.darkIcon == ""
                                                ? <PhotoOutlined color={isItemSelected ? "primary" : "inherit"} sx={{ marginRight: "15px" }} />
                                                : (palette.mode == "dark"
                                                    ? <img
                                                        style={{ width: "20px", height: "20px", objectFit: "contain", marginRight: "15px" }}
                                                        src={isItemSelected ? row.activeIcon : row.darkIcon}
                                                        alt="categoryIcon"
                                                    />
                                                    : <img
                                                        style={{ width: "20px", height: "20px", objectFit: "contain", marginRight: "15px" }}
                                                        src={isItemSelected ? row.activeIcon : row.lightIcon}
                                                        alt="categoryIcon"
                                                    />)
                                            }
                                            <Typography variant="h4" color={isItemSelected ? "primary" : "inherit"}>{row.name}</Typography>
                                        </Box>
                                    </LinkRouter>
                                );
                            })}
                        </Paper>
                    </Grid>
                    <Grid item xs={1}>
                        <Divider sx={{ height: "622px", borderColor: "inherit", my: "24px", mr: "20px" }} orientation="vertical" />
                    </Grid>
                    <Grid item xs={8} sx={{ maxHeight: "622px", my: "24px", display: "flex", flexDirection: "column", flexWrap: "wrap" }}>
                        {fullCatalogItems[selectedCategory]?.children && fullCatalogItems[selectedCategory].children.map((childF, indexF) => {
                            return (
                                <Box key={`$catalog_children_f_${indexF}`} sx={{ mb: "23px" }}>
                                    <LinkRouter underline="none" color="inherit" to={`/catalog/${childF.urlSlug}`} onClick={handleClose}>
                                        <Typography variant="h4" fontWeight="bold" color="inherit">{childF.name}</Typography>
                                    </LinkRouter>
                                    {childF.children.map((childS, indexS) => {
                                        return (
                                            <LinkRouter key={`$catalog_children_s_${indexS}`} underline="none" color="inherit" to={`/catalog/${childS.urlSlug}`} onClick={handleClose}>
                                                <Typography variant="subtitle1" color="inherit" sx={{ mt: "10px" }}>{childS.name}</Typography>
                                            </LinkRouter>
                                        );
                                    })}
                                </Box>
                            );
                        })}
                    </Grid>
                </Grid>
            </Menu>
        </>
    )
}

export default CatalogMenu