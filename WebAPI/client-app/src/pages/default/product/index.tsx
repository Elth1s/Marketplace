import {
    Box,
    Typography,
    Tab
} from "@mui/material";
import { TabContext, TabList, TabPanel } from "@mui/lab";
import { StarRounded } from '@mui/icons-material';
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from "swiper";

import { useActions } from "../../../hooks/useActions";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

import BreadcrumbsComponent from "../../../components/BreadcrumbsComponent";

import ProductMainPage from "./ProductPage/ProductMainPage";
import ProductReviewsPage from "./ProductPage/ProductReviewsPage";
import ProductCharacteristicsPage from "./ProductPage/ProductCharacteristicsPage";
import AddReview from "./AddReview";

import ProductItem from "../../../components/ProductItem";
import { RatingStyle } from "../../../components/Rating/styled";


const Product = () => {
    const { t } = useTranslation();

    const dataTabs = [
        { label: `${t("pages.product.menu.allAboutTheProduct")}` },
        { label: `${t("pages.product.menu.characteristics")}` },
        { label: `${t("pages.product.menu.reviews")}` },
        { label: `${t("pages.product.menu.question")}` },
    ]

    const { GetProductByUrlSlug, GetSimilarProducts, AddProductInCart, GetBasketItems, GetReviews } = useActions();
    const { parents, product, similarProducts } = useTypedSelector(state => state.product);

    let { urlSlug, menu } = useParams();

    const [valueTab, setValueTab] = useState<string>("0");
    const [page, setPage] = useState<number>(1);
    const [rowsPerPage, setRowsPerPage] = useState<number>(4);

    const navigate = useNavigate();

    useEffect(() => {
        document.title = product.name != "" ? product.name : `${t("pages.product.title")}`;
        if (menu)
            setValueTab(menu);

        getData();
    }, [urlSlug, product.name])

    const addInCart = async () => {
        if (urlSlug) {
            await AddProductInCart(urlSlug)
            await GetBasketItems()
        }
    };

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
        if (urlSlug)
            navigate(`/product/${urlSlug}/${newValue}`)
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

                <Typography variant="h1" sx={{ mt: "30px", mb: "15px" }}>{valueTab === "1" && `${t("pages.product.menu.characteristics")}`}{valueTab === "2" && `${t("pages.product.menu.reviews")}`} {product.name}</Typography>
                <Box sx={{ display: "flex", alignItems: "center" }}>
                    <Typography variant="h4" fontWeight="bold" display="inline" sx={{ marginRight: "70px" }}>{t("pages.product.seller")}: <Typography fontWeight="normal" display="inline" sx={{ fontSize: "20px" }}>{product.shopName}</Typography></Typography>
                    <Typography variant="h4" fontWeight="bold">{t("pages.product.sellerRating")}: </Typography>
                    <RatingStyle
                        sx={{ ml: 1, fontSize: "30px", mr: "40px" }}
                        value={4.5}
                        precision={0.5}
                        readOnly
                        icon={<StarRounded sx={{ fontSize: "30px" }} />}
                        emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                    />
                    {valueTab === "2" && <AddReview
                        getData={async () => {
                            if (urlSlug) {
                                await GetReviews(urlSlug, 1, rowsPerPage)
                                setPage(1);
                            }
                        }} />}
                </Box>

                <TabPanel sx={{ p: "0px" }} value="0" >
                    <ProductMainPage addInCart={addInCart}
                        moveToReview={() => {
                            window.scrollTo({
                                top: 0,
                                behavior: 'smooth'
                            });
                            setValueTab("2");
                            if (urlSlug)
                                navigate(`/product/${urlSlug}/2`)
                        }} />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="1">
                    <ProductCharacteristicsPage addInCart={addInCart} />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="2">
                    <ProductReviewsPage addInCart={addInCart} page={page} rowsPerPage={rowsPerPage} />
                </TabPanel>
            </TabContext>

            <Box>
                <Typography variant="h1" sx={{ mb: "40px" }}>{t("pages.product.similarProducts")}</Typography>
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