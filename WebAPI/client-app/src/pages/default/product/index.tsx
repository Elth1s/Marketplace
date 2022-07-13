import {
    Box,
    Grid,
    Typography,
    Tab
} from "@mui/material";
import { TabContext, TabList, TabPanel } from "@mui/lab";
import { Star } from '@mui/icons-material';
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from "swiper";

import { products, dataTabs } from "./data";

import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

import CardProduct from "../../../components/CardProduct";
import BreadcrumbsComponent from "../../../components/BreadcrumbsComponent";

import ProductMainPage from "./ProductPage/ProductMainPage";
import ProductReviewsPage from "./ProductPage/ProductReviewsPage";
import ProductCharacteristicsPage from "./ProductPage/ProductCharacteristicsPage";
import AddReview from "./AddReview";

import { RatingStyle } from "./styled";

const Product = () => {
    const { GetProductByUrlSlug } = useActions();
    const { parents, product } = useTypedSelector(state => state.product);

    const [valueTab, setValueTab] = useState<string>("0");

    let { urlSlug } = useParams();

    useEffect(() => {
        document.title = "Product";

        getData();
    }, [urlSlug])

    const getData = async () => {
        if (!urlSlug)
            return;
        try {
            await GetProductByUrlSlug(urlSlug)
        } catch (ex) {
        }
    };

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValueTab(newValue);
    };

    return (
        <>
            <BreadcrumbsComponent parents={parents} />
            <TabContext value={valueTab} >
                <TabList onChange={handleChange} aria-label="basic tabs example" >
                    {dataTabs.map((item, index) => (
                        <Tab key={index} label={item.label} value={index.toString()} sx={{ padding: "0px", minWidth: "auto", marginRight: "50px", "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }} />
                    ))}
                </TabList >

                <Typography variant="h1" sx={{ mt: "30px", mb: "15px" }}>{product.name}</Typography>
                <Box sx={{ display: "flex", alignItems: "center", mb: "50px" }}>
                    <Typography variant="h4" fontWeight="bold" display="inline" sx={{ marginRight: "70px" }}>Shop: <Typography fontWeight="normal" display="inline" sx={{ fontSize: "20px" }}>{product.shopName}</Typography></Typography>
                    <Typography variant="h4" fontWeight="bold">Shop rating: </Typography>
                    <RatingStyle
                        sx={{ ml: 1, color: "primary" }}
                        value={4.5}
                        precision={0.5}
                        readOnly
                        emptyIcon={<Star fontSize="inherit" />}
                    />
                    {valueTab === "1" && <AddReview />}
                </Box>

                <TabPanel sx={{ p: "0px" }} value="0" >
                    <ProductMainPage urlSlug={urlSlug} isInBasket={product.isInBasket} />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="1">
                    <ProductReviewsPage />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="2">
                    <ProductCharacteristicsPage />
                </TabPanel>
            </TabContext>

            <Grid container sx={{ mb: "55px" }}>
                <Grid item xs={12}>
                    <Typography variant="h4" sx={{ mb: "40px" }}>Схожі товари</Typography>
                </Grid>
                <Grid item xs={12}>
                    <Swiper
                        modules={[Navigation]}
                        navigation={true}
                        slidesPerView={5}
                        slidesPerGroup={1}
                        spaceBetween={15}
                    >
                        {products.map((item, index) => (
                            <SwiperSlide key={index}>
                                <CardProduct
                                    image={item.image}
                                    name={item.title}
                                    statusName={item.status}
                                    price={1000}
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>
                </Grid>
            </Grid>
        </>
    );
}

export default Product;