import { Box, Button, Checkbox, Typography } from '@mui/material';
import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';

import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from "swiper";

import BreadcrumbsComponent from '../../../components/BreadcrumbsComponent';
import CatalogItem from '../../../components/CatalogItem';

import { useActions } from '../../../hooks/useActions';
import { useTypedSelector } from '../../../hooks/useTypedSelector';
import { BoxCatalogStyle, BoxFilterStyle, BoxProductStyle } from './styled';
import CardProduct from '../../../components/CardProduct';
import ProductItem from '../../../components/ProductItem';
import { CheckBoxOutlineBlank } from '@mui/icons-material';

const CatalogWithProducts = () => {
    const { GetParents, GetCatalogWithProducts, GetFiltersByCategory } = useActions();
    const { parents, name, catalogItems, products, filterNames } = useTypedSelector(state => state.catalog);

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(40);
    const [filters, setFilters] = useState<Array<number>>([]);

    let { urlSlug } = useParams();

    useEffect(() => {
        document.title = name;

        getData();
    }, [urlSlug])

    const getData = async () => {
        if (!urlSlug)
            return;
        try {
            await GetParents(urlSlug)
            await GetCatalogWithProducts(urlSlug, page, rowsPerPage, filters);
            await GetFiltersByCategory(urlSlug)
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

    const isSelected = (id: number) => filters.indexOf(id) !== -1;


    return (
        <Box>
            <BreadcrumbsComponent parents={parents} />
            <Typography variant='h1' sx={{ marginBottom: "30px" }}>
                {name}
            </Typography>
            {catalogItems.length != 0
                ? <BoxCatalogStyle>
                    <Swiper
                        modules={[Navigation]}
                        navigation={true}
                        slidesPerView={5}
                        slidesPerGroup={5}
                        spaceBetween={15}
                    >
                        {catalogItems.map((row, index) => {
                            return (
                                <SwiperSlide key={index}>
                                    <CatalogItem name={row.name} image={row.image} urlSlug={row.urlSlug} />
                                </SwiperSlide>
                            );
                        })}
                    </Swiper>
                    {/* {catalogItems.map((row, index) => {
                        return (
                            <CatalogItem key={index} name={row.name} image={row.image} urlSlug={row.urlSlug} />
                        );
                    })} */}
                    {/* Products by catalogItems */}
                </BoxCatalogStyle>
                :
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
            }

        </Box>
    );
}

export default CatalogWithProducts

