import { Box, IconButton, Typography } from "@mui/material"
import { FC, useEffect, useState } from "react"

import { useActions } from "../../hooks/useActions"

import { small_empty } from "../../assets/backgrounds"
import { basket_trash, minus, plus } from "../../assets/icons"
import LinkRouter from "../LinkRouter"
import { TextFieldStyle } from "./styled"

interface Props {
    id: number
    count: number
    image: string
    name: string
    price: number
    productCount: number
    urlSlug: string
    closeBasket: any
    linkUrlSlug: string | undefined
}

const BasketItem: FC<Props> = ({ id, count, image, name, price, productCount, urlSlug, closeBasket, linkUrlSlug }) => {
    const { GetBasketItems, UpdateBasketItem, RemoveFromBasket } = useActions();

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
                style={{ width: "90px", height: "90px", objectFit: "contain" }}
                src={image != "" ? image : small_empty}
                alt="productImage"
            />
            <Box>
                <LinkRouter underline="hover" color="inherit" to={`/product/${urlSlug}`} onClick={closeBasket}>
                    <Typography variant="h6" sx={{ width: "250px" }}>
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
                            src={minus}
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
                            src={plus}
                            alt="plus"
                        />
                    </IconButton>
                </Box>
            </Box>
            <Box>
                <Typography variant="h4">
                    {price} &#8372;
                </Typography>
            </Box>
            <IconButton
                sx={{ borderRadius: '12px', p: 0.5 }}
                size="large"
                aria-label="search"
                color="inherit"
                onClick={async () => {
                    await RemoveFromBasket(id, urlSlug == linkUrlSlug ? true : false);
                    await GetBasketItems();
                }}
            >
                <img
                    style={{ width: "30px", height: "30px" }}
                    src={basket_trash}
                    alt="icon"
                />
            </IconButton>
        </Box>
    )
}

export default BasketItem