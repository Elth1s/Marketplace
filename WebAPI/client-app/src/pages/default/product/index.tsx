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
import ProductQuestionsPage from "./ProductPage/ProductQuestionsPage";
import ProductCharacteristicsPage from "./ProductPage/ProductCharacteristicsPage";
import AddReview from "./AddReview";

import ProductItem from "../../../components/ProductItem";
import { RatingStyle } from "../../../components/Rating/styled";


const Product = () => {
    const { t } = useTranslation();

    const { GetProductByUrlSlug, GetSimilarProducts, AddProductInCart, GetBasketItems, GetReviews } = useActions();
    const { parents, product, productRating } = useTypedSelector(state => state.product);
    const { products } = useTypedSelector(state => state.catalog);

    const dataTabs = [
        { label: `${t("pages.product.menu.allAboutTheProduct")}` },
        { label: `${t("pages.product.menu.characteristics")}` },
        { label: `${productRating.countReviews ? t("pages.product.menu.reviews") : t("pages.product.menu.leaveReview")}` },
        { label: `${t("pages.product.menu.questions")}` },
    ]


    let { urlSlug, menu } = useParams();

    const [valueTab, setValueTab] = useState<string>("0");

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
                        <Tab
                            key={index}
                            label={item.label}
                            value={index.toString()}
                            sx={{
                                fontSize: "20px",
                                padding: "0px",
                                minWidth: "auto",
                                textTransform: "none",
                                color: "inherit",
                                marginRight: "90px",
                                "&& .MuiTouchRipple-child": {
                                    backgroundColor: "transparent"
                                }
                            }}
                        />
                    ))}
                </TabList>

                <TabPanel sx={{ p: "0px" }} value="0" >
                    <ProductMainPage
                        addInCart={addInCart}
                        moveToReview={
                            () => {
                                window.scrollTo({
                                    top: 0,
                                    behavior: 'smooth'
                                });
                                setValueTab("2");
                                if (urlSlug)
                                    navigate(`/product/${urlSlug}/2`)
                            }
                        }
                    />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="1">
                    <ProductCharacteristicsPage addInCart={addInCart} />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="2">
                    <ProductReviewsPage addInCart={addInCart} />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="3">
                    <ProductQuestionsPage addInCart={addInCart} />
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
                    {products.map((row, index) => {
                        return (
                            <SwiperSlide key={index}>
                                <ProductItem isSelected={row.isSelected} name={row.name} image={row.image} statusName={row.statusName} price={row.price} discount={row.discount} urlSlug={row.urlSlug} />
                            </SwiperSlide>
                        );
                    })}
                </Swiper>
            </Box>
        </>
    );
}

export default Product;