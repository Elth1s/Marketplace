import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";
import Tab from "@mui/material/Tab";

import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";
import TabPanel from "@mui/lab/TabPanel";

import StarIcon from '@mui/icons-material/Star';

import React, { useState } from "react";

import { Swiper, SwiperSlide } from 'swiper/react';
import { Navigation } from "swiper";

import ProductMainPage from "./ProductPage/ProductMainPage";
import ProductReviewsPage from "./ProductPage/ProductReviewsPage";
import ProductCharacteristicsPage from "./ProductPage/ProductCharacteristicsPage";
import CardProduct from "../../../components/CardProduct";

import { product, dataTabs } from "./data";
import "./style.css";

const Product = () => {
    const [valueTab, setValueTab] = useState<string>("0");

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValueTab(newValue);
    };

    return (
        <>
            <TabContext value={valueTab} >
                <Box>
                    <TabList onChange={handleChange} aria-label="basic tabs example" >
                        {dataTabs.map((item, index) => (
                            <Tab key={index} label={item.label} value={index.toString()} />
                        ))}
                    </TabList >
                </Box>

                <Box sx={{ mb: "50px" }}>
                    <Typography variant="h4" sx={{ mt: "30px", mb: "15px" }}>Намисто з коштовностями</Typography>
                    <Box
                        sx={{
                            display: "flex",
                            alignItems: "center",
                        }}>
                        <Typography variant="h6" sx={{ mr: "150px" }}>Продавець: GHOP</Typography>
                        <Typography variant="h6">Рейтинг:</Typography>
                        <Rating
                            value={4.5}
                            precision={0.5}
                            readOnly
                            emptyIcon={<StarIcon fontSize="inherit" />} />
                    </Box>
                </Box>

                <TabPanel sx={{ p: "0px" }} value="0" >
                    <ProductMainPage />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="1">
                    <ProductReviewsPage />
                </TabPanel>
                <TabPanel sx={{ p: "0px" }} value="2">
                    <ProductCharacteristicsPage />
                </TabPanel>
            </TabContext>

            <Grid container>
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
                        {product.map((item, index) => (
                            <SwiperSlide key={index}>
                                <CardProduct
                                    image={item.image}
                                    title={item.title}
                                    status={item.status}
                                    price={item.price}
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