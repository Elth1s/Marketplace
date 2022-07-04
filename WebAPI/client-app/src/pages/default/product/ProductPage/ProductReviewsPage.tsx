import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import CardMedia from "@mui/material/CardMedia";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";

import StarIcon from '@mui/icons-material/Star';

import { useState } from "react";

import { Swiper, SwiperSlide } from "swiper/react";
import { Controller, Navigation, Thumbs } from "swiper";

import {
    PriceBox,
    RatingBox,
} from "../styled";
import { images, reviews } from "../data";

const ProductReviewsPage = () => {
    const [thumbsSwiper, setThumbsSwiper] = useState<any>(null);

    return (
        <>
            <Grid container>
                <Grid item xs={8}>
                    {reviews.map((item, index) => (
                        <Card key={index} sx={{ border: "1px solid #000", borderRadius: "10px", mb: "20px", p: "35px" }}>
                            <CardContent>
                                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                    <Typography variant="h4">{item.name}</Typography>
                                    <Box>
                                        <Rating
                                            value={item.rating}
                                            precision={0.5}
                                            readOnly
                                            emptyIcon={<StarIcon fontSize="inherit" />} />
                                        <Typography variant="body1">{item.data}</Typography>
                                    </Box>
                                </Box>
                                <Typography variant="h6" sx={{ mt: "20px" }}>{item.decs}</Typography>
                            </CardContent>
                        </Card>
                    ))}
                </Grid>
                <Grid item xs={4} sx={{ mb: "20px", pl: "100px" }}>
                    <Swiper
                        modules={[Controller, Navigation, Thumbs]}
                        navigation
                        spaceBetween={15}
                        thumbs={{ swiper: thumbsSwiper }}
                    >
                        {images.map((item, index) => (
                            <SwiperSlide key={index}>
                                <CardMedia
                                    component="img"
                                    width="420px"
                                    height="420px"
                                    image={item}
                                    alt="product"
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>
                    <Swiper
                        modules={[Controller, Thumbs]}
                        watchSlidesProgress
                        slidesPerView={3}
                        spaceBetween={30}
                        onSwiper={setThumbsSwiper}
                        style={{
                            marginTop: "20px",
                        }}
                    >
                        {images.map((item, index) => (
                            <SwiperSlide key={index}>
                                <CardMedia
                                    component="img"
                                    width="120px"
                                    height="120px"
                                    image={item}
                                    alt="product"
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>
                    <PriceBox>
                        <Typography variant="h1" sx={{ mt: "50px", mb: "25px" }}>230грн</Typography>
                        <RatingBox>
                            <Rating
                                value={4.5}
                                precision={0.5}
                                readOnly
                                emptyIcon={<StarIcon fontSize="inherit" />} />
                            <Typography>5(10)</Typography>
                        </RatingBox>
                        <Button fullWidth variant="contained" sx={{ fontSize: "27px", py: "15px", mt: "50px", mb: "25px" }}>Купити</Button>
                        <Button fullWidth variant="outlined" sx={{ fontSize: "27px", py: "15px" }}>Контакти продавця</Button>
                    </PriceBox>
                </Grid>
            </Grid>
        </>
    );
}

export default ProductReviewsPage;