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

import ShowInfo from "../../ShortSellerInfo"
import AddReview from "../AddReview";
import ReviewsForm from "../ReviewForm"

interface Props {
    addInCart: any,
}

const ProductReviewsPage: FC<Props> = ({ addInCart }) => {
    const { t } = useTranslation();

    const { product, reviews } = useTypedSelector(state => state.product);
    const { GetReviews, GetMoreReviews } = useActions();

    let { urlSlug } = useParams();

    const [page, setPage] = useState<number>(1);
    const [rowsPerPage, setRowsPerPage] = useState<number>(4);

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
            <Typography variant="h1" sx={{ mt: "30px", mb: "15px" }}>{t("pages.product.menu.reviews")} {product.name}</Typography>
            <Box sx={{ display: "flex", alignItems: "center" }}>
                <Typography variant="h4" fontWeight="bold" display="inline" sx={{ marginRight: "70px" }}>{t("pages.product.seller")}: <Typography fontWeight="normal" display="inline" sx={{ fontSize: "20px" }}>{product.shopName}</Typography></Typography>
                <Typography variant="h4" fontWeight="bold">{t("pages.product.sellerRating")}: </Typography>
                <RatingStyle
                    sx={{ ml: 1, fontSize: "30px", mr: "40px" }}
                    value={product.shopRating}
                    precision={0.1}
                    readOnly
                    icon={<StarRounded sx={{ fontSize: "30px" }} />}
                    emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                />
                <AddReview
                    getData={async () => {
                        if (urlSlug) {
                            await GetReviews(urlSlug, 1, rowsPerPage)
                            setPage(1);
                        }
                    }}
                />
            </Box>
            <Grid container sx={{ mt: "79px" }}>
                <Grid item xs={8}>
                    {(reviews.length === 0) ? (
                        <>
                            <Typography variant="h1">{t("pages.product.reviews")}</Typography>
                            <ReviewsForm getData={getData} />
                        </>
                    ) : (
                        <>
                            {reviews?.length != 0 && reviews.map((item, index) => {
                                return (
                                    <ReviewItem
                                        key={index}
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
                        </>
                    )}
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
                                    value={product.productRating}
                                    precision={0.1}
                                    readOnly
                                    icon={<StarRounded sx={{ fontSize: "30px" }} />}
                                    emptyIcon={<StarRounded sx={{ fontSize: "30px" }} />}
                                />
                                <Typography variant="h4" fontWeight="bold" display="inline">{product.productRating} <Typography fontWeight="medium" display="inline" sx={{ fontSize: "20px" }}>({product.countReviews} {t("pages.product.ratings")})</Typography></Typography>
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