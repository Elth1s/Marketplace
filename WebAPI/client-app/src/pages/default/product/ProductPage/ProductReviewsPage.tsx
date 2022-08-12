import { Box, Grid, Typography, IconButton } from "@mui/material";
import { StarRounded } from "@mui/icons-material";

import { FC, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import {
    BuyButtonSecondStyle,
    PriceBox,
} from "../styled";
import { RatingStyle } from "../../../../components/Rating/styled";

import { buy_cart, orange_heart } from "../../../../assets/icons";

import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import ReviewItem from "../../../../components/ReviewItem";
import { useParams } from "react-router-dom";
import { useActions } from "../../../../hooks/useActions";

import ShowInfo from "../../ShopInfo"


interface Props {
    addInCart: any,
    page: number,
    rowsPerPage: number
}

const ProductReviewsPage: FC<Props> = ({ addInCart, page, rowsPerPage }) => {
    const { t } = useTranslation();

    const { product, reviews } = useTypedSelector(state => state.product);
    const { GetReviews, GetMoreReviews } = useActions();

    let { urlSlug } = useParams();

    useEffect(() => {

        getData();
    }, [])

    const getData = async () => {
        if (!urlSlug)
            return;
        try {
            await GetReviews(urlSlug, page, rowsPerPage)
        } catch (ex) {
        }
    };

    return (
        <>
            <Grid container sx={{ mt: "79px" }}>
                <Grid item xs={8}>
                    {reviews?.length != 0 && reviews.map((item, index) => {
                        return (
                            <ReviewItem
                                fullName={item.fullName}
                                reviewLink="/"
                                date={item.date}
                                productRating={item.productRating}
                                comment={item.comment}
                                advantages={item.advantages}
                                disadvantages={item.disadvantages}
                                images={item.images}
                                videoURL={item.videoURL}
                                isLiked={item.isLiked}
                                isDisliked={item.isDisliked}
                                likes={item.likes}
                                dislikes={item.dislikes}
                                replies={item.replies}
                            />
                        )
                    })}
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{ width: "420px", ml: "auto" }}>
                        {product.images?.length > 0 && <img
                            style={{ width: "420px", height: "420px", objectFit: "contain" }}
                            src={product.images[0].name}
                            alt="productImage"
                        />}
                        <PriceBox>
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
                                <Typography variant="h4" fontWeight="bold" display="inline">5 <Typography fontWeight="medium" display="inline" sx={{ fontSize: "20px" }}>(10 {t("pages.product.ratings")})</Typography></Typography>
                            </Box>
                            {product.isInBasket
                                ? <BuyButtonSecondStyle fullWidth color="secondary" variant="contained" disabled
                                    startIcon={
                                        <img
                                            style={{ width: "40px", height: "40px" }}
                                            src={buy_cart}
                                            alt="icon"
                                        />}
                                    sx={{ mt: "47px" }}
                                >
                                    {t("pages.product.inBasket")}
                                </BuyButtonSecondStyle>
                                : <BuyButtonSecondStyle fullWidth color="secondary" variant="contained"
                                    startIcon={
                                        <img
                                            style={{ width: "40px", height: "40px" }}
                                            src={buy_cart}
                                            alt="icon"
                                        />}
                                    sx={{ mt: "47px" }}
                                    onClick={addInCart}
                                >
                                    {t("pages.product.buy")}
                                </BuyButtonSecondStyle>}
                            <ShowInfo isMainPage={false} id={product.shopId} />
                        </PriceBox>
                    </Box>
                </Grid>
            </Grid>
        </>
    );
}

export default ProductReviewsPage;