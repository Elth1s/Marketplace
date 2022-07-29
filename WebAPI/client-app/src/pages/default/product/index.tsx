import {
    Box,
    Grid,
    Typography,
    Tab
} from "@mui/material";
import { TabContext, TabList, TabPanel } from "@mui/lab";
import { Star, StarRounded } from '@mui/icons-material';
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from "swiper";

import { products } from "./data";

import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

import CardProduct from "../../../components/CardProduct";
import BreadcrumbsComponent from "../../../components/BreadcrumbsComponent";

import ProductMainPage from "./ProductPage/ProductMainPage";
import ProductReviewsPage from "./ProductPage/ProductReviewsPage";
import ProductCharacteristicsPage from "./ProductPage/ProductCharacteristicsPage";
import AddReview from "./AddReview";

import { RatingStyle } from "./styled";
import ProductItem from "../../../components/ProductItem";

const dataTabs = [
    { label: 'All about the product' },
    { label: 'Characteristics' },
    { label: 'Reviews' },
    { label: 'Question' },
]

const Product = () => {
    const { GetProductByUrlSlug, GetSimilarProducts } = useActions();
    const { parents, product, similarProducts } = useTypedSelector(state => state.product);

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
            await GetSimilarProducts(urlSlug)
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
                        <Tab key={index} label={item.label} value={index.toString()} sx={{ fontSize: "20px", padding: "0px", minWidth: "auto", textTransform: "none", marginRight: "90px", "&& .MuiTouchRipple-child": { backgroundColor: "transparent" } }} />
                    ))}
                </TabList >

                <Typography variant="h1" sx={{ mt: "30px", mb: "15px" }}>{valueTab === "1" && "Characteristics"} {product.name}</Typography>
                <Box sx={{ display: "flex", alignItems: "center" }}>
                    <Typography variant="h4" fontWeight="bold" display="inline" sx={{ marginRight: "70px" }}>Shop: <Typography fontWeight="normal" display="inline" sx={{ fontSize: "20px" }}>{product.shopName}</Typography></Typography>
                    <Typography variant="h4" fontWeight="bold">Shop rating: </Typography>
                    <RatingStyle
                        sx={{ ml: 1, fontSize: "30px" }}
                        value={4.5}
                        precision={0.5}
                        readOnly
                        icon={<StarRounded sx={{ fontSize: "30px" }} />}
                        emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                    />
                    {valueTab === "2" && <AddReview />}
                </Box>

                <TabPanel sx={{ p: "0px" }} value="0" >
                    <ProductMainPage urlSlug={urlSlug} isInBasket={product.isInBasket}
                        moveToReview={() => {
                            window.scrollTo({
                                top: 0,
                                behavior: 'smooth'
                            });
                            setValueTab("2");
                        }} />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="1">
                    <ProductCharacteristicsPage urlSlug={urlSlug} isInBasket={product.isInBasket} />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="2">
                    <ProductReviewsPage />
                </TabPanel>
            </TabContext>

            <Box>
                <Typography variant="h1" sx={{ mb: "40px" }}>Similar products</Typography>
                <Swiper
                    modules={[Navigation]}
                    navigation={true}
                    slidesPerView={5}
                    slidesPerGroup={5}
                    spaceBetween={15}
                >
                    {similarProducts.map((row, index) => {
                        return (
                            <SwiperSlide key={index}>
                                <ProductItem name={row.name} image={row.image} statusName={row.statusName} price={row.price} urlSlug={row.urlSlug} />
                            </SwiperSlide>
                        );
                    })}
                </Swiper>
            </Box>
        </>
    );
}

export default Product;