import {
    alpha,
    Box,
    Button,
    Divider,
    Grid,
    Menu,
    Paper,
    Typography
} from "@mui/material";
import { useEffect, useState } from "react";

import { useActions } from "../../hooks/useActions";
import { useTypedSelector } from "../../hooks/useTypedSelector";

import { CategoryTypographyStyle } from "./styled"

import { list, white_close } from "../../assets/icons";
import { empty } from "../../assets/backgrounds";
import LinkRouter from "../LinkRouter";

const CatalogMenu = () => {
    const { GetFullCatalog } = useActions();
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
                    }
                }}
                startIcon={
                    <img
                        style={{ width: "20px", height: "20px" }}
                        src={open ? white_close : list}
                        alt="icon"
                    />
                }
            >
                Catalog
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
                                    <LinkRouter key={`$catalog_${index}`} underline="none" to={`/catalog/${row.urlSlug}`} onClick={handleClose}>
                                        <Box sx={{ display: "flex", mb: "14px", alignItems: "center" }} onMouseEnter={() => changeParentCategory(index)}>
                                            <img
                                                style={{ width: "20px", height: "20px", objectFit: "contain", marginRight: "13px" }}
                                                src={row.icon != "" ? row.icon : empty}
                                                alt="categoryIcon"
                                            />
                                            <CategoryTypographyStyle variant="h4" fontWeight="bold" selected={isItemSelected}>{row.name}</CategoryTypographyStyle>
                                        </Box>
                                    </LinkRouter>
                                );
                            })}
                        </Paper>
                    </Grid>
                    <Grid item xs={1}>
                        <Divider sx={{ height: "622px", borderColor: "black", my: "24px", mr: "20px" }} orientation="vertical" />
                    </Grid>
                    <Grid item xs={8} sx={{ maxHeight: "622px", my: "24px", display: "flex", flexDirection: "column", flexWrap: "wrap" }}>
                        {fullCatalogItems[selectedCategory]?.children && fullCatalogItems[selectedCategory].children.map((childF, indexF) => {
                            return (
                                <Box key={`$catalog_children_f_${indexF}`} sx={{ mb: "23px" }}>
                                    <LinkRouter underline="none" to={`/catalog/${childF.urlSlug}`} onClick={handleClose}>
                                        <CategoryTypographyStyle variant="h4" fontWeight="bold">{childF.name}</CategoryTypographyStyle>
                                    </LinkRouter>
                                    {childF.children.map((childS, indexS) => {
                                        return (
                                            <LinkRouter key={`$catalog_children_s_${indexS}`} underline="none" to={`/catalog/${childS.urlSlug}`} onClick={handleClose}>
                                                <CategoryTypographyStyle variant="subtitle1" sx={{ mt: "10px" }}>{childS.name}</CategoryTypographyStyle>
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