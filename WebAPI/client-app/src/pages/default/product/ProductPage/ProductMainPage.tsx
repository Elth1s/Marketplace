import { Box, Grid, Typography, IconButton } from "@mui/material";
import { StarRounded } from "@mui/icons-material";
import { FC, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { Swiper, SwiperSlide } from "swiper/react";
import { Controller, Navigation, Thumbs } from "swiper";

import AddReview from "../AddReview";

import {
    CharacteristicDivider,
    SellerContactsButton,
    BuyButton,
    ListItemStyle,
    ListStyle,
    CharacteristicGrid,
} from "../styled";
import { RatingStyle } from "../../../../components/Rating/styled";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import { arrow_right, buy_cart, credit_card, dollar_sign, filled_orange_heart, orange_heart, package_delivery, truck_delivery } from "../../../../assets/icons";
import { useActions } from "../../../../hooks/useActions";

import ShowInfo from "../../ShortSellerInfo"
import ReviewsForm from "../ReviewForm"
import { big_empty } from "../../../../assets/backgrounds";
import LinkRouter from "../../../../components/LinkRouter";

interface Props {
    moveToReview: any
    addInCart: any
}

const ProductMainPage: FC<Props> = ({ addInCart, moveToReview }) => {
    const { t } = useTranslation();

    const { GetReviews, BasketMenuChange, AddProductInSelected, GetProductRatingByUrlSlug } = useActions();
    const { product, reviews, productRating } = useTypedSelector(state => state.product);

    let { urlSlug } = useParams();

    const [thumbsSwiper, setThumbsSwiper] = useState<any>(null);

    useEffect(() => {

        getData();
    }, [])


    const getData = async () => {
        if (!urlSlug)
            return;
        try {
            await GetReviews(urlSlug, 1, 3)
            await GetProductRatingByUrlSlug(urlSlug);
        } catch (ex) {
        }
    };

    return (
        <>
            <Typography variant="h1" sx={{ mt: "30px", mb: "15px" }}>{product.name}</Typography>
            <Box sx={{ display: "flex", alignItems: "center" }}>
                <Typography variant="h4" fontWeight="bold" display="inline" sx={{ marginRight: "70px" }}>{t("pages.product.seller")}:&nbsp;
                    <Typography
                        fontWeight="normal"
                        display="inline"
                        sx={{ fontSize: "20px" }}
                    >
                        <LinkRouter underline="hover" color="inherit" to={`/seller-info/${product.shopId}`}>
                            {product.shopName}
                        </LinkRouter>
                    </Typography>
                </Typography>
                <Typography variant="h4" fontWeight="bold">{t("pages.product.sellerRating")}: </Typography>
                <RatingStyle
                    sx={{ ml: 1, fontSize: "30px", mr: "40px" }}
                    value={product.shopRating}
                    precision={0.1}
                    readOnly
                    icon={<StarRounded sx={{ fontSize: "30px" }} />}
                    emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                />
            </Box>
            <Grid container sx={{ mb: "80px" }}>
                <Grid item xs={4} sx={{ mt: "84px" }}>
                    {product.images?.length != 0
                        ? <>
                            <Swiper
                                modules={[Controller, Navigation, Thumbs]}
                                navigation
                                spaceBetween={15}
                                thumbs={{ swiper: thumbsSwiper }}
                            >
                                {product.images.map((item, index) => (
                                    <SwiperSlide key={index} style={{ width: "520px", height: "520px" }}>
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
                                    <SwiperSlide key={index} style={{ width: "65px", height: "65px" }}>
                                        <img
                                            style={{ width: "63px", height: "63px", objectFit: "contain" }}
                                            src={item.name}
                                            alt="productImage"
                                        />
                                    </SwiperSlide>
                                ))}
                            </Swiper>}
                        </>
                        : <img
                            style={{ width: "520px", height: "620px", objectFit: "contain" }}
                            src={big_empty}
                            alt="productImage"
                        />}
                </Grid>
                <Grid item xs={4} sx={{ mt: "84px", display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "start", pl: "119px" }}>
                    <Box sx={{ display: "flex", alignItems: 'baseline' }}>
                        <Typography fontSize="50px" lineHeight="63px" sx={{ mr: "35px" }}>{product.price} &#8372;</Typography>
                        <IconButton color="primary" sx={{ borderRadius: "12px" }}>
                            <img
                                style={{ width: "35px", height: "35px" }}
                                src={product.isSelected ? filled_orange_heart : orange_heart}
                                alt="icon"
                                onClick={() => {
                                    if (urlSlug)
                                        AddProductInSelected(urlSlug)
                                }}
                            />
                        </IconButton>
                    </Box>
                    <Box sx={{ display: "flex", mt: "26px", alignItems: "center" }}>
                        <RatingStyle
                            sx={{ mr: 1, fontSize: "30px" }}
                            value={productRating.rating}
                            precision={0.1}
                            readOnly
                            icon={<StarRounded sx={{ fontSize: "30px" }} />}
                            emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                        />
                        <Typography variant="h4" fontWeight="bold" display="inline">{productRating.rating} <Typography fontWeight="medium" display="inline" sx={{ fontSize: "20px" }}>({productRating.countReviews} {t("pages.product.ratings")})</Typography></Typography>
                    </Box>
                    <ShowInfo isMainPage={true} id={product.shopId} />
                    {product.isInBasket
                        ? <BuyButton color="secondary" variant="contained"
                            startIcon={
                                <img
                                    style={{ width: "40px", height: "40px" }}
                                    src={buy_cart}
                                    alt="icon"
                                />}
                            sx={{ mt: "35px" }}
                            onClick={BasketMenuChange}
                        >
                            {t("pages.product.inBasket")}
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
                            {t("pages.product.buy")}
                        </BuyButton>}
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{ width: "500px", ml: "auto" }}>
                        <Typography variant="h1">
                            {t("pages.product.payment")}
                        </Typography>
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

                        <Typography variant="h1" sx={{ marginTop: "80px" }}>
                            {t("pages.product.delivery")}
                        </Typography>
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
                    <Typography variant="h1" lineHeight="56px" sx={{ mb: "40px" }}>
                        {t("pages.product.characterictics")}
                    </Typography>
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
                        {(reviews.length === 0) ? (
                            <>
                                <Typography variant="h1">{t("pages.product.reviews")}</Typography>
                                <ReviewsForm getData={getData} />
                            </>
                        ) : (
                            <>
                                <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", mb: "40px" }}>
                                    <Typography variant="h1">  {t("pages.product.reviews")} </Typography>
                                    <AddReview getData={getData} />
                                </Box>
                                {reviews?.length != 0 && reviews.map((item, index) => {
                                    return (
                                        <Box key={`main_page_review_${index}`} sx={{ border: "1px solid #7e7e7e", borderRadius: "10px", mb: "20px", px: "33px", pt: "35px", pb: "34px" }}>
                                            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                                <Typography variant="h1">{item.fullName}</Typography>
                                                <Box>
                                                    <Typography variant="h5" align="center">{item.date}</Typography>
                                                    <RatingStyle
                                                        value={item.productRating}
                                                        precision={0.5}
                                                        readOnly
                                                        sx={{ fontSize: "30px" }}
                                                        icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                                        emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                                    />
                                                </Box>
                                            </Box>
                                            <Typography variant="h4" sx={{ mt: "21px" }}>{item.comment}</Typography>
                                        </Box>
                                    )
                                })}
                                <Box sx={{ width: "100%", display: "flex", justifyContent: "end" }} >
                                    <Box sx={{ display: "flex", cursor: "pointer" }} onClick={moveToReview}>
                                        <Typography variant='h4' color="#7e7e7e">
                                            {t("pages.product.moreReviews")}
                                        </Typography>
                                        <img
                                            style={{ width: "24px", height: "24px", marginTop: "auto" }}
                                            src={arrow_right}
                                            alt="icon"
                                        />
                                    </Box>
                                </Box>
                            </>
                        )}
                    </Box>
                </Grid>
            </Grid>
        </>
    );
}

export default ProductMainPage;