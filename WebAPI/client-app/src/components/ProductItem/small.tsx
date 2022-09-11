import { Box, IconButton, Typography, Paper } from '@mui/material'
import { FC } from 'react'

import LinkRouter from '../LinkRouter'
import { SmallBoxStyle, SmallImageBoxStyle } from './styled'

import { small_empty } from '../../assets/backgrounds'
import { FavoriteBorder } from '@mui/icons-material'

import { orange_heart, filled_orange_heart, in_shopping_cart, orange_shopping_cart } from '../../assets/icons';
import { useActions } from '../../hooks/useActions'
import { useTranslation } from 'react-i18next'

interface Props {
    isInCart: boolean,
    isSelected: boolean,
    name: string,
    image: string,
    urlSlug: string,
    statusName: string,
    price: number,
}

const ProductItem: FC<Props> = ({ isInCart, isSelected, name, image, statusName, urlSlug, price }) => {
    const { t } = useTranslation();

    const { ChangeIsSelectedUserProducts, AddProductInCart, GetBasketItems, ChangeIsInCartUserProducts } = useActions();

    const addInCart = async () => {
        if (!isInCart) {
            await AddProductInCart(urlSlug)
            await ChangeIsInCartUserProducts(urlSlug)
            await GetBasketItems()
        }
    }

    return (
        <Box sx={{ height: "300px", marginRight: "5px", marginBottom: "5px", position: "relative" }}>
            <LinkRouter underline="none" color="unset" to={`/product/${urlSlug}`}
                onClick={() => {
                    window.scrollTo({
                        top: 0,
                        behavior: 'smooth'
                    });
                }}>
                <SmallBoxStyle>
                    <SmallImageBoxStyle>
                        <img
                            style={{ width: "160px", height: "160px", objectFit: "contain" }}
                            src={image != "" ? image : small_empty}
                            alt="productImage"
                        />
                    </SmallImageBoxStyle>
                    <Paper elevation={0} sx={{ minHeight: "36px", maxHeight: "36px", overflow: "hidden" }}>
                        <Typography variant="subtitle1" fontWeight="medium">
                            {name}
                        </Typography>
                    </Paper>
                    <Typography fontSize="12" color="secondary" fontWeight="medium" py="5px">
                        {statusName}
                    </Typography>
                    <Typography fontSize="12" sx={{ marginTop: "auto" }}>
                    </Typography>
                    <Typography variant="subtitle1">
                        {price} {t("currency")}
                    </Typography>
                </SmallBoxStyle>
            </LinkRouter>
            <Box
                sx={{
                    position: "absolute",
                    zIndex: 2,
                    bottom: "3%",
                    right: "3%"
                }}
            >
                <IconButton
                    color="primary"
                    sx={{
                        borderRadius: "12px",
                    }}
                    onClick={() => ChangeIsSelectedUserProducts(urlSlug)}
                >
                    <img
                        style={{ width: "20px", height: "20px" }}
                        src={isSelected ? filled_orange_heart : orange_heart}
                        alt="icon"
                    />
                </IconButton>
                <IconButton
                    color="primary"
                    sx={{
                        borderRadius: "12px",
                    }}
                    onClick={addInCart}
                >
                    <img
                        style={{ width: "20px", height: "20px" }}
                        src={isInCart ? in_shopping_cart : orange_shopping_cart}
                        alt="icon"
                    />
                </IconButton>
            </Box>
        </Box>
    )
}

export default ProductItem;