import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Grid from "@mui/material/Grid";
import Rating from "@mui/material/Rating";
import Typography from "@mui/material/Typography";
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';

import { FC, useState } from "react";

import { Swiper, SwiperSlide } from "swiper/react";
import { Controller, Navigation, Thumbs } from "swiper";

import AddReview from "../AddReview";

import {
    CharacteristicTypography,
    CharacteristicDivider,
    RatingStyle,
    SellerContactsButton,
    BuyButton,
    ListItemStyle,
    ListStyle,
    CharacteristicGrid,
} from "../styled";
import { reviews } from "../data";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";
import { arrow_right, buy_cart, credit_card, dollar_sign, orange_heart, package_delivery, truck_delivery } from "../../../../assets/icons";
import { IconButton } from "@mui/material";
import { Star, StarRounded } from "@mui/icons-material";

interface Props {
    urlSlug: string | undefined,
    isInBasket: boolean,
    moveToReview: any
}

const ProductMainPage: FC<Props> = ({ urlSlug, isInBasket, moveToReview }) => {
    const { AddProductInCart, GetBasketItems } = useActions();
    const { product } = useTypedSelector(state => state.product);

    const [thumbsSwiper, setThumbsSwiper] = useState<any>(null);

    const addInCart = async () => {
        if (urlSlug) {
            await AddProductInCart(urlSlug)
            await GetBasketItems()
        }
    };

    return (
        <>
            <Grid container sx={{ mb: "80px" }}>
                <Grid item xs={4} sx={{ mt: "84px" }}>
                    <Swiper
                        modules={[Controller, Navigation, Thumbs]}
                        navigation
                        spaceBetween={15}
                        thumbs={{ swiper: thumbsSwiper }}
                    >
                        {product.images.map((item, index) => (
                            <SwiperSlide key={index}>
                                <img
                                    style={{ width: "520px", height: "520px", objectFit: "contain" }}
                                    src={item.name}
                                    alt="productImage"
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>
                    {product.images?.length > 1 && <Swiper
                        modules={[Controller, Thumbs]}
                        watchSlidesProgress
                        slidesPerView={6}
                        spaceBetween={25}
                        onSwiper={setThumbsSwiper}
                        style={{
                            marginTop: "35px",
                        }}
                    >
                        {product.images.map((item, index) => (
                            <SwiperSlide key={index}>
                                <img
                                    style={{ width: "65px", height: "65px", objectFit: "contain" }}
                                    src={item.name}
                                    alt="productImage"
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>}
                </Grid>
                <Grid item xs={4} sx={{ mt: "84px", display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "start", pl: "119px" }}>
                    <Box sx={{ display: "flex", alignItems: 'baseline' }}>
                        <Typography fontSize="64px" lineHeight="74px" sx={{ mr: "35px" }}>{product.price} &#8372;</Typography>
                        <IconButton color="primary" sx={{ borderRadius: "12px" }}>
                            <img
                                style={{ width: "50px", height: "50px" }}
                                src={orange_heart}
                                alt="icon"
                            />
                        </IconButton>
                    </Box>
                    <Box sx={{ display: "flex", mt: "26px", alignItems: "center" }}>
                        <RatingStyle
                            sx={{ mr: 1, fontSize: "30px" }}
                            value={5}
                            precision={0.5}
                            readOnly
                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                        />
                        <Typography variant="h4" fontWeight="bold" display="inline">5 <Typography fontWeight="medium" display="inline" sx={{ fontSize: "20px" }}>(10 ratings)</Typography></Typography>
                    </Box>
                    <SellerContactsButton color="secondary" variant="outlined" sx={{ mt: "41px" }}>Seller contacts</SellerContactsButton>
                    {isInBasket
                        ? <BuyButton color="secondary" variant="contained" disabled
                            startIcon={
                                <img
                                    style={{ width: "40px", height: "40px" }}
                                    src={buy_cart}
                                    alt="icon"
                                />}
                            sx={{ mt: "35px" }}
                        >
                            In basket
                        </BuyButton>
                        : <BuyButton color="secondary" variant="contained"
                            startIcon={
                                <img
                                    style={{ width: "40px", height: "40px" }}
                                    src={buy_cart}
                                    alt="icon"
                                />}
                            sx={{ mt: "35px" }}
                            onClick={addInCart}
                        >
                            Buy
                        </BuyButton>}
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{ width: "500px", ml: "auto" }}>
                        <Typography variant="h1">Payment</Typography>
                        <ListStyle>
                            <ListItemStyle>
                                <img
                                    style={{ width: "30px", height: "30px", marginRight: "70px" }}
                                    src={credit_card}
                                    alt="icon"
                                />
                                <Typography variant="h4" fontWeight="medium">
                                    Postpaid «Nova Poshta»
                                </Typography>
                            </ListItemStyle>
                            <ListItemStyle>
                                <img
                                    style={{ width: "30px", height: "30px", marginRight: "70px" }}
                                    src={credit_card}
                                    alt="icon"
                                />
                                <Typography variant="h4" fontWeight="medium">
                                    Payment by details
                                </Typography>
                            </ListItemStyle>
                            <ListItemStyle>
                                <img
                                    style={{ width: "30px", height: "30px", marginRight: "70px" }}
                                    src={dollar_sign}
                                    alt="icon"
                                />
                                <Typography variant="h4" fontWeight="medium">
                                    Cash
                                </Typography>
                            </ListItemStyle>
                        </ListStyle>

                        <Typography variant="h1" sx={{ marginTop: "80px" }}>Delivery</Typography>
                        <ListStyle>
                            <ListItemStyle>
                                <img
                                    style={{ width: "30px", height: "30px", marginRight: "70px" }}
                                    src={package_delivery}
                                    alt="icon"
                                />
                                <Typography variant="h4" fontWeight="medium">
                                    Delivery «Nova Poshta»
                                </Typography>
                            </ListItemStyle>
                            <ListItemStyle>
                                <img
                                    style={{ width: "30px", height: "30px", marginRight: "70px" }}
                                    src={truck_delivery}
                                    alt="icon"
                                />
                                <Typography variant="h4" fontWeight="medium">
                                    Delivery «Mall»
                                </Typography>
                            </ListItemStyle>
                            <ListItemStyle>
                                <img
                                    style={{ width: "30px", height: "30px", marginRight: "70px" }}
                                    src={truck_delivery}
                                    alt="icon"
                                />
                                <Typography variant="h4" fontWeight="medium">
                                    Ukrposhta
                                </Typography>
                            </ListItemStyle>
                        </ListStyle>
                    </Box>
                </Grid>
            </Grid>

            <Grid container sx={{ mb: "42px" }}>
                <Grid item xs={7}>
                    <Typography variant="h1" lineHeight="56px" sx={{ mb: "40px" }}>Characterictics</Typography>
                    {product.filters.map((item, index) => (
                        <CharacteristicGrid container columns={7} key={index} >
                            <Grid item xs={5}>
                                <Typography variant="h4" fontWeight="bold" sx={{ display: "inline", background: "#fff", pr: 1 }}>{item.filterName}</Typography>
                                <CharacteristicDivider />
                            </Grid>
                            <Grid item xs={2}>
                                <Typography variant="h4" align="left" sx={{ background: "#fff", pl: 1 }}>{item.value} {item.unitMeasure}</Typography>
                            </Grid>
                        </CharacteristicGrid>
                    ))}
                </Grid>

                <Grid item xs={5}>
                    <Box sx={{ width: "590px", ml: "auto" }}>
                        <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", mb: "40px" }}>
                            <Typography variant="h1">Review</Typography>
                            <AddReview />
                        </Box>
                        {reviews.map((item, index) => (
                            <Box key={index} sx={{ border: "1px solid #7e7e7e", borderRadius: "10px", mb: "20px", px: "33px", pt: "35px", pb: "34px" }}>
                                <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                    <Typography variant="h1">{item.name}</Typography>
                                    <Box>
                                        <Typography variant="h5" align="center">{item.data}</Typography>
                                        <RatingStyle
                                            value={item.rating}
                                            precision={0.5}
                                            readOnly
                                            sx={{ fontSize: "30px" }}
                                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                        />
                                    </Box>
                                </Box>
                                <Typography variant="h4" sx={{ mt: "21px" }}>{item.decs}</Typography>
                            </Box>
                        ))}
                        <Box sx={{ width: "100%", display: "flex", justifyContent: "end" }} >
                            <Box sx={{ display: "flex", cursor: "pointer" }} onClick={moveToReview}>
                                <Typography variant='h4' color="#7e7e7e">
                                    More reviews
                                </Typography>
                                <img
                                    style={{ width: "24px", height: "24px", marginTop: "auto" }}
                                    src={arrow_right}
                                    alt="icon"
                                />
                            </Box>
                        </Box>
                    </Box>
                </Grid>
            </Grid>
        </>
    );
}

export default ProductMainPage;