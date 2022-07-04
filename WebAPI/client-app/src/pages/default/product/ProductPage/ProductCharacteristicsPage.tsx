import Button from "@mui/material/Button";
import Grid from "@mui/material/Grid";
import CardMedia from "@mui/material/CardMedia";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";

import StarIcon from '@mui/icons-material/Star';

import { useState } from "react";

import { Swiper, SwiperSlide } from "swiper/react";
import { Controller, Navigation, Thumbs } from "swiper";

import {
    PriceBox,
    RatingBox,
    CharacteristicBox,
    CharacteristicTypography,
    CharacteristicDivider,
} from "../styled";
import { characteristic, images } from "../data";

const ProductCharacteristicsPage = () => {
    const [thumbsSwiper, setThumbsSwiper] = useState<any>(null);

    return (
        <Grid container>
            <Grid item xs={8}>
                {characteristic.map((item, index) => (
                    <CharacteristicBox key={index} >
                        <CharacteristicTypography>{item.name}</CharacteristicTypography>
                        <CharacteristicDivider />
                        <CharacteristicTypography>{item.value}</CharacteristicTypography>
                    </CharacteristicBox>
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
    );
}

export default ProductCharacteristicsPage;