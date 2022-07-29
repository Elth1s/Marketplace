import { Box, Button, Checkbox, Pagination, Typography } from '@mui/material';
import { CachedOutlined, CheckBoxOutlineBlank } from '@mui/icons-material';
import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';

import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from "swiper";

import BreadcrumbsComponent from '../../../components/BreadcrumbsComponent';
import CatalogItem from '../../../components/CatalogItem';

import { useActions } from '../../../hooks/useActions';
import { useTypedSelector } from '../../../hooks/useTypedSelector';
import { BoxCatalogStyle, BoxFilterStyle, BoxProductStyle, PaginationItemStyle, ShowMoreButton } from './styled';
import ProductItem from '../../../components/ProductItem';

const CatalogWithProducts = () => {
    const { GetParents, GetCatalogWithProducts, GetMoreProducts, GetFiltersByCategory } = useActions();
    const { parents, name, catalogItems, products, countProducts, filterNames } = useTypedSelector(state => state.catalog);

    const [page, setPage] = useState(1);
    const [catalogPage, setCatalogPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(40);
    const [filters, setFilters] = useState<Array<number>>([]);

    let { urlSlug } = useParams();

    useEffect(() => {
        document.title = name;
        getData();
    }, [urlSlug, page])

    const getData = async () => {
        if (!urlSlug)
            return;
        try {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
            await GetParents(urlSlug)
            await GetCatalogWithProducts(urlSlug, page, rowsPerPage, filters);
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

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const isSelected = (id: number) => filters.indexOf(id) !== -1;


    return (
        <Box>
            <BreadcrumbsComponent parents={parents} />
            <Typography variant='h1' sx={{ marginBottom: "30px" }}>
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
                                <ProductItem key={row.urlSlug} name={row.name} image={row.image} statusName={row.statusName} price={row.price} urlSlug={row.urlSlug} />
                            );
                        })}
                    </BoxCatalogStyle>
                    {products.length != countProducts && <Box sx={{ display: "flex", justifyContent: "center" }}>
                        <ShowMoreButton onClick={showMore} startIcon={<CachedOutlined />}>
                            Show more
                        </ShowMoreButton>
                    </Box>}
                </>
                : <>
                    <Box sx={{ display: "flex" }}>
                        <BoxFilterStyle>
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
                                Find
                            </Button>
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
            }

        </Box >
    );
}

export default CatalogWithProducts

