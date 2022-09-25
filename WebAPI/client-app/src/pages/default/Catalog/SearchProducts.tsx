import { CheckBoxOutlineBlank, ExpandMore, PhotoOutlined } from "@mui/icons-material";
import { Accordion, AccordionDetails, AccordionSummary, Box, Button, Checkbox, Pagination, Typography, useTheme } from "@mui/material"
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import { grid_active, grid_dark, grid_light } from "../../../assets/icons";
import ProductItem from "../../../components/ProductItem";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { BoxFilterStyle, BoxProductStyle, PaginationItemStyle } from "./styled";




const SearchProducts = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { GetCategoriesForSearch, GetFiltersByCategoryIdForUser, SearchProductsForUser, ResetCatalogFilters } = useActions();
    const { products, countProducts, filterNames, searchCatalog } = useTypedSelector(state => state.catalog);

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(40);
    const [filters, setFilters] = useState<Array<number>>([]);
    const [categories, setCategories] = useState<Array<number>>([]);

    let { productName } = useParams();

    useEffect(() => {
        if (productName)
            document.title = productName;

        getData();
    }, [page, productName, categories])

    const getData = async () => {
        if (!productName)
            return;
        try {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
            await SearchProductsForUser(productName, page, rowsPerPage, categories, filters)

            if (categories?.length == 0)
                await ResetCatalogFilters();

            if (categories?.length == 1)
                await GetFiltersByCategoryIdForUser(categories[0])
        } catch (ex) {
        }
    };

    useEffect(() => {

        getCategories();
    }, [productName])

    const getCategories = async () => {
        if (!productName)
            return;
        try {
            await GetCategoriesForSearch(productName);
        } catch (ex) {
        }
    };

    const filterHandleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const index = filters.indexOf(id);
        const tmpList = filters.slice();

        if (index === -1)
            tmpList.push(id)
        else
            tmpList.splice(index, 1);

        setFilters(tmpList);
    };

    const categoryHandleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const tmpList: Array<number> = [];

        if (id != 0)
            tmpList.push(id)

        setFilters([]);
        setCategories(tmpList);
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const lastCharOfCountProducts = (countProducts: number) => {
        let stringCountProducts = countProducts.toString();
        let lastChar = stringCountProducts[stringCountProducts.length - 1];
        let twoLastChar = stringCountProducts.slice(-2);
        if (twoLastChar == "12" || twoLastChar == "13" || twoLastChar == "14")
            return false
        else if (lastChar == "2" || lastChar == "3" || lastChar == "4")
            return true;
        else
            return false
    };

    const isSelected = (id: number) => filters.indexOf(id) !== -1;

    return (
        <>
            <Typography variant="h2" color="inherit" display="inline">
                &laquo;
                <Typography display="inline" color="inherit" sx={{ fontSize: "30px", lineHeight: "38px" }}>
                    {productName}
                </Typography>
                &raquo;
            </Typography>
            <Typography variant="h4" color="inherit">
                {t("pages.catalog.finded")} {countProducts} {countProducts == 1 ? t("pages.ordering.countProductsOne") : (lastCharOfCountProducts(countProducts) ? t("pages.ordering.countProductsLessFive") : t("pages.ordering.countProducts"))}
            </Typography>
            <Box sx={{ display: "flex", mt: "20px" }}>
                <BoxFilterStyle>
                    <Box
                        sx={{
                            display: "flex",
                            cursor: "pointer"
                        }}
                        onClick={(event) => categoryHandleClick(event, 0)}
                    >
                        <Box sx={{ display: "flex", alignItems: "center" }}>
                            {palette.mode == "dark"
                                ? <img
                                    style={{ width: "20px", height: "20px", objectFit: "contain" }}
                                    src={categories.length == 0 ? grid_active : grid_light}
                                    alt="allProducts"
                                />
                                : <img
                                    style={{ width: "20px", height: "20px", objectFit: "contain" }}
                                    src={categories.length == 0 ? grid_active : grid_dark}
                                    alt="allProducts"
                                />
                            }
                        </Box>
                        <Typography variant="h4" fontWeight="bold" color={categories.length == 0 ? "primary" : "inherit"} sx={{ pl: "10px" }}>
                            {t("pages.catalog.allProducts")}
                        </Typography>
                    </Box>
                    {searchCatalog?.length != 0
                        && <>
                            {searchCatalog.map((parent, index) => {
                                return (
                                    <Accordion key={`parent_category_${index}`} sx={{ pt: "15px", boxShadow: 0, "&:before": { background: "inherit" }, "&.Mui-expanded": { m: 0 } }}>
                                        <AccordionSummary
                                            expandIcon={<ExpandMore />}
                                            sx={{
                                                p: 0,
                                                m: 0,
                                                minHeight: "25px",
                                                ".MuiAccordionSummary-content": {
                                                    m: 0,
                                                    "&.Mui-expanded": {
                                                        m: 0
                                                    }
                                                },
                                                "&.Mui-expanded": {
                                                    minHeight: "25px",
                                                }
                                            }}>
                                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                                {parent.lightIcon == "" || parent.darkIcon == ""
                                                    ? <PhotoOutlined color="inherit" sx={{ fontSize: "20px" }} />
                                                    : (palette.mode == "dark"
                                                        ? <img
                                                            style={{ width: "20px", height: "20px", objectFit: "contain" }}
                                                            src={parent.lightIcon}
                                                            alt="categoryIcon"
                                                        />
                                                        : <img
                                                            style={{ width: "20px", height: "20px", objectFit: "contain" }}
                                                            src={parent.darkIcon}
                                                            alt="categoryIcon"
                                                        />
                                                    )
                                                }
                                            </Box>
                                            <Typography variant="h4" color="inherit" fontWeight="bold" sx={{ pl: "10px" }}>
                                                {parent.name}
                                            </Typography>
                                        </AccordionSummary>
                                        <AccordionDetails sx={{ p: 0 }}>
                                            {
                                                parent.children?.length != 0
                                                && <>
                                                    {parent.children.map((child, index) => {
                                                        console.log(categories.length != 0 && categories[0] == child.id)
                                                        return (
                                                            <Typography
                                                                key={`child_category_${index}`}
                                                                variant="h4"
                                                                sx={{
                                                                    mt: "10px",
                                                                    cursor: "pointer"
                                                                }}
                                                                onClick={(event) => categoryHandleClick(event, child.id)}
                                                                color={(categories.length != 0 && categories[0] == child.id) ? "primary" : "inherit"}
                                                            >
                                                                {child.name} ({child.countProducts})
                                                            </Typography>
                                                        )
                                                    })}
                                                </>
                                            }
                                        </AccordionDetails>
                                    </Accordion>
                                )
                            })}
                        </>
                    }
                    {filterNames?.length != 0
                        && <>
                            {filterNames.map((item) => {
                                return (
                                    <Box key={`fn${item.id}`} sx={{ paddingTop: "40px" }}>
                                        <Typography variant="h3" color="inherit" fontWeight="bold" sx={{ paddingBottom: "5px" }} >
                                            {item.name} {item.unitMeasure != null && `(${item.unitMeasure})`}
                                        </Typography>
                                        {item.filterValues.map((filterValue) => {
                                            const isItemSelected = isSelected(filterValue.id);

                                            return (
                                                <Box key={`fv${filterValue.id}`}
                                                    sx={{ display: "flex", justifyContent: "start", alignItems: "center", cursor: "pointer" }}
                                                    onClick={(event) => filterHandleClick(event, filterValue.id)}
                                                    role="checkbox"
                                                    aria-checked={isItemSelected}>
                                                    <Checkbox
                                                        icon={<CheckBoxOutlineBlank color="primary" />}
                                                        color="primary"
                                                        sx={{
                                                            pl: 0,
                                                            "&:hover": { background: "transparent" },
                                                            "&& .MuiTouchRipple-child": { backgroundColor: "transparent" }
                                                        }}
                                                        checked={isItemSelected}
                                                    />
                                                    <Typography variant="h4" color="inherit">
                                                        {filterValue.value}
                                                    </Typography>
                                                </Box>
                                            )
                                        })}
                                    </Box>
                                )
                            })}
                            <Button
                                color="secondary"
                                variant="contained"
                                sx={{
                                    borderRadius: "8px",
                                    fontSize: "20px",
                                    p: 1,
                                    mt: 2
                                }}
                                onClick={() => { getData() }}
                            >
                                {t("pages.catalog.find")}
                            </Button>
                        </>
                    }
                </BoxFilterStyle>
                <BoxProductStyle>
                    {products.map((row, index) => {
                        return (
                            <ProductItem key={row.urlSlug} isSelected={row.isSelected} name={row.name} discount={row.discount} image={row.image} statusName={row.statusName} price={row.price} urlSlug={row.urlSlug} />
                        );
                    })}
                </BoxProductStyle>
            </Box>
            {products?.length > 0 && <Box sx={{ display: "flex", justifyContent: "center" }}>
                <Pagination count={Math.ceil(countProducts / rowsPerPage)} page={page} shape="rounded" size="large" showFirstButton showLastButton
                    onChange={(event: any, value: number) => {
                        handleChangePage(event, value);
                    }}
                    renderItem={(item) => <PaginationItemStyle {...item} />} />
            </Box>}
        </>
    )
}

export default SearchProducts