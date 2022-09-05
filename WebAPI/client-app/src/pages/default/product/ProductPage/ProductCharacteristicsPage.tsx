import {
    Grid,
    Box,
    Typography,
    IconButton
} from "@mui/material";
import { StarRounded } from '@mui/icons-material';
import { FC } from "react";
import { useTranslation } from "react-i18next";

import {
    PriceBox,
    CharacteristicDivider,
    CharacteristicGrid,
    BuyButtonSecondStyle,
} from "../styled";
import { RatingStyle } from "../../../../components/Rating/styled";

import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { buy_cart, orange_heart } from "../../../../assets/icons";

import ShowInfo from "../../ShortSellerInfo"

interface Props {
    addInCart: any
}

const ProductCharacteristicsPage: FC<Props> = ({ addInCart }) => {
    const { t } = useTranslation();

    const { product } = useTypedSelector(state => state.product);

    return (
        <>
            <Typography variant="h1" sx={{ mt: "30px", mb: "15px" }}>{t("pages.product.menu.characteristics")} {product.name}</Typography>
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
            </Box>
            <Grid container sx={{ mt: "40px" }}>
                <Grid item xs={7}>
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
                <Grid item xs={1} />
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

export default ProductCharacteristicsPage;