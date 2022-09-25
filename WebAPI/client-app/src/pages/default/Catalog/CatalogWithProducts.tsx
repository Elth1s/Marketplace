import { Box, Button, Checkbox, Pagination, Typography } from '@mui/material';
import { CachedOutlined, CheckBoxOutlineBlank } from '@mui/icons-material';
import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import { useTranslation } from 'react-i18next';

import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from "swiper";

import BreadcrumbsComponent from '../../../components/BreadcrumbsComponent';
import CatalogItem from '../../../components/CatalogItem';

import { useActions } from '../../../hooks/useActions';
import { useTypedSelector } from '../../../hooks/useTypedSelector';
import { BoxCatalogStyle, BoxFilterStyle, BoxProductStyle, PaginationItemStyle, ShowMoreButton, TextFieldStyle } from './styled';
import ProductItem from '../../../components/ProductItem';
import { TextFieldSecondStyle } from '../../../components/TextField/styled';

const CatalogWithProducts = () => {
    const { t } = useTranslation();

    const { GetParents, GetCatalogWithProducts, GetMoreProducts, GetFiltersByCategory } = useActions();
    const { parents, name, catalogItems, products, countProducts, filterNames, min, max } = useTypedSelector(state => state.catalog);

    const [page, setPage] = useState(1);
    const [catalogPage, setCatalogPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(40);
    const [filters, setFilters] = useState<Array<number>>([]);

    const [minPrice, setMinPrice] = useState<number>(min);
    const [maxPrice, setMaxPrice] = useState<number>(max);


    let { urlSlug } = useParams();

    useEffect(() => {
        document.title = name;
        getData();
    }, [urlSlug, page, name])

    const getData = async () => {
        if (!urlSlug)
            return;
        try {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
            await GetParents(urlSlug)
            await GetCatalogWithProducts(urlSlug, minPrice, maxPrice, page, rowsPerPage, filters);
            if (minPrice == 0 || maxPrice == 0) {
                setMinPrice(min)
                setMaxPrice(max)
            }
            await GetFiltersByCategory(urlSlug)
        } catch (ex) {
        }
    };

    const showMore = async () => {
        if (!urlSlug)
            return;
        try {
            let newPage = catalogPage + 1;
            await GetMoreProducts(urlSlug, newPage)
            setCatalogPage(newPage);
        } catch (ex) {

        }
    }

    const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const index = filters.indexOf(id);
        const tmpList = filters.slice();

        if (index === -1)
            tmpList.push(id)
        else
            tmpList.splice(index, 1);

        setFilters(tmpList);
    };

    const changeMinPrice = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const numberRegex = /^[1-9][0-9]*$/;
        let value = e.target.value;

        if (value == "") {
            setMinPrice(min)
            return
        }

        if (numberRegex.test(value))
            if (+value > maxPrice) {
                setMinPrice(maxPrice - 1);
            }
            else {
                setMinPrice(+value);
            }
    }

    const changeMaxPrice = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const numberRegex = /^[1-9][0-9]*$/;
        let value = e.target.value;

        if (value == "") {
            setMaxPrice(max)
            return
        }

        if (numberRegex.test(value))
            setMaxPrice(+value);
        // if (+value < minPrice) {
        //     setMaxPrice(minPrice + 1);
        // }
        // else {
        // }
    }

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const isSelected = (id: number) => filters.indexOf(id) !== -1;


    return (
        <Box>
            <BreadcrumbsComponent parents={parents} />
            <Typography variant='h1' color="inherit" sx={{ marginBottom: "30px" }}>
                {name}
            </Typography>
            {catalogItems.length != 0
                ? <>
                    < Swiper
                        modules={[Navigation]}
                        navigation={true}
                        spaceBetween={15}
                        breakpoints={{
                            1185: {
                                slidesPerGroup: 4,
                                slidesPerView: 4,
                            },
                            1560: {
                                slidesPerGroup: 5,
                                slidesPerView: 5,
                            },
                        }}
                    >
                        {catalogItems.map((row, index) => {
                            return (
                                <SwiperSlide key={index}>
                                    <CatalogItem name={row.name} image={row.image} urlSlug={row.urlSlug} />
                                </SwiperSlide>
                            );
                        })}
                    </Swiper>
                    <BoxCatalogStyle>
                        {products != null && products.map((row, index) => {
                            return (
                                <ProductItem key={row.urlSlug} isSelected={row.isSelected} name={row.name} image={row.image} statusName={row.statusName} price={row.price} discount={row.discount} urlSlug={row.urlSlug} />
                            );
                        })}
                    </BoxCatalogStyle>
                    {products.length != countProducts && <Box sx={{ display: "flex", justifyContent: "center" }}>
                        <ShowMoreButton onClick={showMore} startIcon={<CachedOutlined />}>
                            {t("pages.catalog.showMore")}
                        </ShowMoreButton>
                    </Box>}
                </>
                : <>
                    <Box sx={{ display: "flex" }}>
                        <BoxFilterStyle>
                            <Typography variant="h3" color="inherit" fontWeight="bold" >
                                {t("pages.catalog.price")}
                            </Typography>
                            <Box
                                sx={{
                                    pt: "20px",
                                    display: "flex"
                                }}
                            >
                                <TextFieldStyle
                                    value={minPrice}
                                    sx={{ width: "75px" }}
                                    onChange={changeMinPrice}
                                />
                                <Typography color="inherit">
                                    &nbsp;&nbsp;-&nbsp;&nbsp;
                                </Typography>
                                <TextFieldStyle
                                    value={maxPrice}
                                    sx={{ width: "75px" }}
                                    onChange={changeMaxPrice}
                                />
                            </Box>
                            {filterNames.map((item) => {
                                return (
                                    <Box key={`fn${item.id}`} sx={{ paddingTop: "50px" }}>
                                        <Typography variant="h3" color="inherit" fontWeight="bold" sx={{ paddingBottom: "5px" }} >
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
                                onClick={() => {
                                    if (page != 1)
                                        setPage(1)
                                    else
                                        getData();
                                }}
                            >
                                {t("pages.catalog.find")}
                            </Button>
                        </BoxFilterStyle>
                        <BoxProductStyle>
                            {products.map((row, index) => {
                                return (
                                    <ProductItem key={row.urlSlug} isSelected={row.isSelected} name={row.name} image={row.image} discount={row.discount} statusName={row.statusName} price={row.price} urlSlug={row.urlSlug} />
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
            }

        </Box >
    );
}

export default CatalogWithProducts

