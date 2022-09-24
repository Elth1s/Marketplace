import { CheckBoxOutlineBlank, ExpandMore, PhotoOutlined } from "@mui/icons-material";
import { Accordion, AccordionDetails, AccordionSummary, Box, Button, Checkbox, Pagination, Typography, useTheme } from "@mui/material"
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import ProductItem from "../../../../../components/ProductItem";
import { useActions } from "../../../../../hooks/useActions";
import { useTypedSelector } from "../../../../../hooks/useTypedSelector";
import { BoxFilterStyle, BoxProductStyle, PaginationItemStyle } from "../../../Catalog/styled";




const SellerProducts = () => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { GetCategoriesByShopId, GetFiltersByCategoryIdForUser, SearchSellerProducts } = useActions();
    const { products, countProducts, filterNames, searchCatalog } = useTypedSelector(state => state.catalog);

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(40);
    const [filters, setFilters] = useState<Array<number>>([]);
    const [categories, setCategories] = useState<Array<number>>([]);

    let { shopId } = useParams();

    useEffect(() => {

        getData();
    }, [page, categories])

    const getData = async () => {
        if (!shopId)
            return;
        try {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
            await SearchSellerProducts(shopId, page, rowsPerPage, categories, filters)

            if (categories?.length == 1)
                await GetFiltersByCategoryIdForUser(categories[0])
        } catch (ex) {
        }
    };

    useEffect(() => {

        getCategories();
    }, [])

    const getCategories = async () => {
        if (!shopId)
            return;
        try {
            await GetCategoriesByShopId(shopId);
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

        tmpList.push(id)

        setFilters([]);
        setCategories(tmpList);
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const isSelected = (id: number) => filters.indexOf(id) !== -1;

    return (
        <>
            <Box sx={{ display: "flex" }}>
                <BoxFilterStyle>
                    {searchCatalog?.length != 0
                        && <>
                            {searchCatalog.map((parent, index) => {
                                return (
                                    <Accordion key={`sc${index}`} sx={{ pt: "15px", boxShadow: 0, "&:before": { background: "inherit" }, "&.Mui-expanded": { m: 0 } }}>
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
                                                        return (
                                                            <Typography
                                                                variant="h4"
                                                                color="inherit"
                                                                sx={{
                                                                    mt: "10px",
                                                                    cursor: "pointer"
                                                                }}
                                                                onClick={(event) => categoryHandleClick(event, child.id)}
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
                            <ProductItem isSelected={row.isSelected} key={row.urlSlug} discount={row.discount} name={row.name} image={row.image} statusName={row.statusName} price={row.price} urlSlug={row.urlSlug} />
                        );
                    })}
                </BoxProductStyle>
            </Box>
            <Box sx={{ display: "flex", justifyContent: "center" }}>
                <Pagination count={Math.ceil(countProducts / rowsPerPage)} page={page} shape="rounded" size="large" showFirstButton showLastButton
                    onChange={(event: any, value: number) => {
                        handleChangePage(event, value);
                    }}
                    renderItem={(item) => <PaginationItemStyle {...item} />} />
            </Box>
        </>
    )
}

export default SellerProducts