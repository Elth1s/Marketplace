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

import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import CreditCardIcon from '@mui/icons-material/CreditCard';
import StarIcon from '@mui/icons-material/Star';

import { FC, useState } from "react";

import { Swiper, SwiperSlide } from "swiper/react";
import { Controller, Navigation, Thumbs } from "swiper";

import AddReview from "../AddReview";

import {
    CharacteristicBox,
    CharacteristicTypography,
    CharacteristicDivider,
} from "../styled";
import { characteristic, images, reviews } from "../data";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { useActions } from "../../../../hooks/useActions";

interface Props {
    urlSlug: string | undefined,
    isInBasket: boolean
}

const ProductMainPage: FC<Props> = ({ urlSlug, isInBasket }) => {
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
                <Grid item xs={4}>
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
                                    src={item}
                                    alt="productImage"
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>
                    <Swiper
                        modules={[Controller, Thumbs]}
                        watchSlidesProgress
                        slidesPerView={3}
                        spaceBetween={65}
                        onSwiper={setThumbsSwiper}
                        style={{
                            marginTop: "25px",
                        }}
                    >
                        {product.images.map((item, index) => (
                            <SwiperSlide key={index}>
                                <img
                                    style={{ width: "130px", height: "130px", objectFit: "contain" }}
                                    src={item}
                                    alt="productImage"
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "flex-start", height: "100%", pl: "80px" }}>
                        <Typography variant="h1">{product.price} &#8372;</Typography>
                        <Box sx={{ display: "flex" }}>
                            <Rating
                                value={4.5}
                                precision={0.5}
                                readOnly
                                emptyIcon={<StarIcon fontSize="inherit" />} />
                            <Typography>5(10)</Typography>
                        </Box>

                        <Button color="secondary" variant="outlined" sx={{ fontSize: "20px", mt: "45px", mb: "35px" }}>Контакти продавця</Button>
                        {isInBasket
                            ? <Button color="secondary" variant="contained" disabled startIcon={<ShoppingCartIcon />} sx={{ fontSize: "20px" }}>
                                In basket
                            </Button>
                            : <Button color="secondary" variant="contained" startIcon={<ShoppingCartIcon />} sx={{ fontSize: "20px" }}
                                onClick={addInCart} >
                                Buy
                            </Button>}
                    </Box>
                </Grid>
                <Grid item xs={4}>
                    <Typography variant="h4">Оплата</Typography>
                    <Card
                        sx={{
                            border: "1px solid #000",
                            borderRadius: "10px",
                            mt: "30px",
                            mb: "80px"
                        }}>
                        <CardContent>
                            <List>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Післяплата «Нова Пошта»" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Оплата за реквізитами" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Готівка" />
                                </ListItem>
                            </List>
                        </CardContent>
                    </Card>
                    <Typography variant="h4">Доставка</Typography>
                    <Card sx={{ border: "1px solid #000", borderRadius: "10px", mt: "30px" }}>
                        <CardContent>
                            <List>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Доставка «Нова Пошта»" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Доставка «Mall»" />
                                </ListItem>
                                <ListItem>
                                    <ListItemIcon><CreditCardIcon /></ListItemIcon>
                                    <ListItemText primary="Укрпошта" />
                                </ListItem>
                            </List>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>

            <Grid container>
                <Grid item xs={6}>
                    <Typography variant="h4" sx={{ mb: "40px" }}>Характеристики</Typography>
                    {product.filters.map((item, index) => (
                        <CharacteristicBox key={index} >
                            <CharacteristicTypography variant="h4">{item.filterName}</CharacteristicTypography>
                            <CharacteristicDivider />
                            <CharacteristicTypography variant="h4" align="left">{item.value} {item.unitMeasure}</CharacteristicTypography>
                        </CharacteristicBox>
                    ))}
                </Grid>
                <Grid item xs={6}>
                    <Box sx={{ pl: "120px" }}>
                        <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", mb: "40px" }}>
                            <Typography variant="h4">Відгук</Typography>
                            <AddReview />
                        </Box>
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
                    </Box>
                </Grid>
            </Grid>
        </>
    );
}

export default ProductMainPage;