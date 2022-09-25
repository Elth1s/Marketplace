import { Box, IconButton, Typography, useTheme } from "@mui/material"
import { FC, useEffect, useState } from "react"

import { useActions } from "../../hooks/useActions"

import { small_empty } from "../../assets/backgrounds"
import { basket_trash, minus, minus_light, plus, plus_light, trash_light } from "../../assets/icons"
import LinkRouter from "../LinkRouter"
import { TextFieldStyle } from "./styled"
import { useTranslation } from "react-i18next"

interface Props {
    id: number
    count: number
    image: string
    name: string
    price: number
    discount: number | null
    productCount: number
    urlSlug: string
    closeBasket: any
    linkUrlSlug: string | undefined
}

const BasketItem: FC<Props> = ({ id, count, image, name, price, discount, productCount, urlSlug, closeBasket, linkUrlSlug }) => {
    const { t } = useTranslation();
    const { palette } = useTheme();

    const { GetBasketItems, UpdateBasketItem, RemoveFromBasket, ChangeIsInCartUserProducts } = useActions();

    const [basketItemCount, setBasketItemCount] = useState<number>(count);

    const changeCount = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const numberRegex = /^[1-9][0-9]*$/;
        let value = e.target.value;

        if (value == "") {
            setBasketItemCount(1)
            return
        }

        if (numberRegex.test(value))
            if (+value > productCount) {
                setBasketItemCount(productCount);
                await UpdateBasketItem(id, productCount);
            }
            else {
                setBasketItemCount(+value);
                await UpdateBasketItem(id, +value);
            }
    }

    const plusMinusCount = async (isPlus: boolean) => {
        let tempCount = basketItemCount;
        if (isPlus)
            tempCount = tempCount + 1;
        else
            tempCount = tempCount - 1;

        setBasketItemCount(tempCount);
        await UpdateBasketItem(id, tempCount);
    }

    return (
        <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", my: "15px" }}>
            <img
                style={{ width: "90px", height: "90px", objectFit: "contain", marginRight: "10px" }}
                src={image != "" ? image : small_empty}
                alt="productImage"
            />
            <Box>
                <LinkRouter underline="hover" color="inherit" to={`/product/${urlSlug}`} onClick={closeBasket}>
                    <Typography variant="h6" color="inherit" sx={{ width: "250px" }}>
                        {name}
                    </Typography>
                </LinkRouter>
                <Box sx={{ display: "flex", alignItems: "center" }}>
                    <IconButton
                        sx={{
                            "&:hover": {
                                background: "transparent"
                            },
                            "&& .MuiTouchRipple-child": {
                                backgroundColor: "transparent"
                            }
                        }}
                        disabled={basketItemCount == 1}
                        onClick={() => plusMinusCount(false)}
                    >
                        <img
                            style={{ width: "20px", height: "20px", cursor: "pointer" }}
                            src={palette.mode != "dark" ? minus : minus_light}
                            alt="minus"
                        />
                    </IconButton>
                    <TextFieldStyle
                        value={basketItemCount}
                        sx={{ width: "38px" }}
                        onChange={changeCount}
                    />
                    <IconButton
                        sx={{
                            "&:hover": {
                                background: "transparent"
                            },
                            "&& .MuiTouchRipple-child": {
                                backgroundColor: "transparent"
                            }
                        }}
                        disabled={basketItemCount == productCount}
                        onClick={() => plusMinusCount(true)}
                    >
                        <img
                            style={{ width: "20px", height: "20px", cursor: "pointer" }}
                            src={palette.mode != "dark" ? plus : plus_light}
                            alt="plus"
                        />
                    </IconButton>
                </Box>
            </Box>
            <Box>
                {discount != null
                    ? <Box sx={{ mr: "35px" }}>
                        <Typography variant="h6" color="#7e7e7e">{price} {t("currency")}</Typography>
                        <Typography variant="h5" color="inherit" sx={{ mt: "5px" }}>{discount} {t("currency")}</Typography>
                    </Box>
                    : <Typography variant="h5" color="inherit" sx={{ mr: "35px" }}>{price} {t("currency")}</Typography>
                }
            </Box>
            <IconButton
                sx={{ borderRadius: '12px', p: 0.5 }}
                size="large"
                aria-label="search"
                color="inherit"
                onClick={async () => {
                    await RemoveFromBasket(id, urlSlug == linkUrlSlug ? true : false);
                    await ChangeIsInCartUserProducts(urlSlug)
                    await GetBasketItems();
                }}
            >
                <img
                    style={{ width: "30px", height: "30px" }}
                    src={palette.mode != "dark" ? basket_trash : trash_light}
                    alt="icon"
                />
            </IconButton>
        </Box>
    )
}

export default BasketItem