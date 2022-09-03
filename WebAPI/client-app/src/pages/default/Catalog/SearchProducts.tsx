import { CheckBoxOutlineBlank, ExpandMore } from "@mui/icons-material";
import { Accordion, AccordionDetails, AccordionSummary, Box, Button, Checkbox, Pagination, Typography } from "@mui/material"
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import ProductItem from "../../../components/ProductItem";
import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import { GetFiltersByCategory } from "./actions";
import { BoxFilterStyle, BoxProductStyle, PaginationItemStyle } from "./styled";




const SearchProducts = () => {
    const { t } = useTranslation();

    const { GetCategoriesForSearch, GetFiltersByCategory, SearchProductsForUser } = useActions();
    const { catalogItems, products, countProducts, filterNames, searchCatalog } = useTypedSelector(state => state.catalog);

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(40);
    const [filters, setFilters] = useState<Array<number>>([]);
    const [categories, setCategories] = useState<Array<number>>([]);

    let { productName } = useParams();

    useEffect(() => {
        if (productName)
            document.title = productName;

        getData();
    }, [page, productName])

    const getData = async () => {
        if (!productName)
            return;
        try {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
            await SearchProductsForUser(productName, page, rowsPerPage, categories, filters)
            await GetCategoriesForSearch(productName);
        } catch (ex) {
        }
    };


    const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const index = filters.indexOf(id);
        const tmpList = filters.slice();

        if (index === -1)
            tmpList.push(id)
        else
            tmpList.splice(index, 1);

        setFilters(tmpList);
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const isSelected = (id: number) => filters.indexOf(id) !== -1;

    return (
        <>
            <Typography variant="h2" display="inline">
                &laquo;
                <Typography display="inline" sx={{ fontSize: "30px", lineHeight: "38px" }}>
                    {productName}
                </Typography>
                &raquo;
            </Typography>
            <Typography variant="h4">
                {t("pages.catalog.finded")} {countProducts} {t("pages.catalog.countProducts")}
            </Typography>
            <Box sx={{ display: "flex", mt: "20px" }}>
                <BoxFilterStyle>
                    {searchCatalog?.length != 0
                        && <>
                            {searchCatalog.map((parent, index) => {
                                return (
                                    <Accordion key={`sc${index}`} sx={{ boxShadow: 0 }}>
                                        <AccordionSummary expandIcon={<ExpandMore />} sx={{ p: 0 }}>
                                            <Typography>
                                                {parent.name}
                                            </Typography>
                                        </AccordionSummary>
                                        <AccordionDetails>
                                            {
                                                parent.children?.length != 0
                                                && <>
                                                    {parent.children.map((child, index) => {
                                                        return (
                                                            <Typography>
                                                                {child.name}
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
                                    <Box key={`fn${item.id}`} sx={{ paddingTop: "50px" }}>
                                        <Typography variant="h3" fontWeight="bold" sx={{ paddingBottom: "5px" }} >
                                            {item.name} {item.unitMeasure != null && `(${item.unitMeasure})`}
                                        </Typography>
                                        {item.filterValues.map((filterValue) => {
                                            const isItemSelected = isSelected(filterValue.id);

                                            return (
                                                <Box key={`fv${filterValue.id}`}
                                                    sx={{ display: "flex", justifyContent: "start", alignItems: "center", cursor: "pointer" }}
                                                    onClick={(event) => handleClick(event, filterValue.id)}
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
                                                    <Typography variant="h4">
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
                            <ProductItem key={row.urlSlug} name={row.name} image={row.image} statusName={row.statusName} price={row.price} urlSlug={row.urlSlug} />
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

export default SearchProducts