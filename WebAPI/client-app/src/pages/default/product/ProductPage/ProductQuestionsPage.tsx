import { Box, Grid, Typography, IconButton } from "@mui/material";
import { CachedOutlined, StarRounded } from "@mui/icons-material";

import { FC, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";

import {
    BuyButtonSecondStyle,
    PriceBox,
} from "../styled";
import { RatingStyle } from "../../../../components/Rating/styled";

import { buy_cart, filled_orange_heart, orange_heart } from "../../../../assets/icons";

import { useTypedSelector } from "../../../../hooks/useTypedSelector";
import ReviewItem from "../../../../components/ReviewItem";
import { useParams } from "react-router-dom";
import { useActions } from "../../../../hooks/useActions";

import ShowInfo from "../../ShortSellerInfo"
import AddQuestion from "../AddQuestion";
import QuestionItem from "../../../../components/QuestionItem";
import LinkRouter from "../../../../components/LinkRouter";
import QuestionForm from "../QuestionForm";
import { big_empty } from "../../../../assets/backgrounds";
import { ShowMoreButton } from "../../Catalog/styled";


interface Props {
    addInCart: any,
}

const ProductReviewsPage: FC<Props> = ({ addInCart }) => {
    const { t } = useTranslation();

    const { GetQuestions, GetMoreQuestions, BasketMenuChange, AddProductInSelected } = useActions();
    const { product, questions, productRating, questionsCount } = useTypedSelector(state => state.product);

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
            await GetQuestions(urlSlug, page, rowsPerPage)
        } catch (ex) {
        }
    };

    const showMore = async () => {
        if (!urlSlug)
            return;
        try {
            let newPage = page + 1;
            await GetMoreQuestions(urlSlug, newPage, rowsPerPage)
            setPage(newPage);
        } catch (ex) {

        }
    }

    return (
        <>
            <Typography variant="h1" sx={{ mt: "30px", mb: "15px" }}>{questions.length !== 0 ? t("pages.product.menu.questions") : t("pages.product.menu.leaveQuestions")} {product.name}</Typography>
            {questions.length !== 0 && <Box sx={{ display: "flex", alignItems: "center" }}>
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
                <AddQuestion
                    getData={async () => {
                        if (urlSlug) {
                            await GetQuestions(urlSlug, 1, rowsPerPage)
                            setPage(1);
                        }
                    }}
                />
            </Box>}
            <Grid container sx={{ mt: "40px" }}>
                <Grid item xs={8}>
                    {questions?.length == 0
                        ? <QuestionForm getData={getData} />
                        : <>
                            {questions.map((item, index) => {
                                return (
                                    <QuestionItem
                                        key={`question_item_${index}`}
                                        id={item.id}
                                        fullName={item.fullName}
                                        questionLink="/"
                                        date={item.date}
                                        message={item.message}
                                        images={item.images}
                                        isLiked={item.isLiked}
                                        isDisliked={item.isDisliked}
                                        likes={item.likes}
                                        dislikes={item.dislikes}
                                        repliesCount={item.repliesCount}
                                        replies={item.replies}
                                        getData={() => getData()}
                                    />
                                )
                            })}
                            {questions.length != questionsCount && <Box sx={{ display: "flex", justifyContent: "center" }}>
                                <ShowMoreButton onClick={showMore} startIcon={<CachedOutlined />}>
                                    {t("pages.catalog.showMore")}
                                </ShowMoreButton>
                            </Box>}
                        </>
                    }
                </Grid>
                <Grid item xs={4}>
                    <Box sx={{ width: "350px", ml: "auto" }}>
                        {product.images?.length > 0
                            ? <img
                                style={{ width: "350px", height: "350px", objectFit: "contain" }}
                                src={product.images[0].name}
                                alt="productImage"
                            />
                            : <img
                                style={{ width: "350px", height: "350px", objectFit: "contain" }}
                                src={big_empty}
                                alt="productImage"
                            />}
                        <PriceBox>
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
                            {product.isInBasket
                                ? <BuyButtonSecondStyle fullWidth color="secondary" variant="contained"
                                    startIcon={
                                        <img
                                            style={{ width: "40px", height: "40px" }}
                                            src={buy_cart}
                                            alt="icon"
                                        />}
                                    sx={{ mt: "47px" }}
                                    onClick={BasketMenuChange}
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