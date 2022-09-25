import Grid from '@mui/material/Grid';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';

import { useTranslation } from 'react-i18next';

import { AdminDialogButton } from '../../../../components/Button/style';
import TextFieldComponent from '../../../../components/TextField';
import DialogTitleWithButton from '../../../../components/Dialog/DialogTitleWithButton';
import IconButtonPlus from '../../../../components/Button/IconButtonPlus';

import { useActions } from "../../../../hooks/useActions";

import { ServerError, UpdateProps } from '../../../../store/types';

import { toLowerFirstLetter } from '../../../../http_comon';
import { FC, useState } from 'react';
import { Box, Button, Typography } from '@mui/material';
import { Img } from '../../../user/styled';
import LinkRouter from '../../../../components/LinkRouter';
import { small_empty } from '../../../../assets/backgrounds';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';
import { Edit } from '@mui/icons-material';

import { orderStatusIdCanceled, orderStatusIdSent, orderStatusIdCompleted, orderStatusIdInProcess } from "../constants"
import UpdateTrackingNumber from '../UpdateTrackingNumber';

const OrderUpdate: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const { GetOrderByIdForSeller, UpdateOrderForSeller } = useActions();
    const { selectedOrder } = useTypedSelector(state => state.order)

    const [open, setOpen] = useState(false);

    const handleClickOpen = async () => {
        setOpen(true);
        await GetOrderByIdForSeller(id)
    };

    const handleClickClose = () => {
        setOpen(false);
        afterUpdate()
    };

    return (
        <>
            <Edit onClick={() => handleClickOpen()} />
            <Dialog
                open={open}
                sx={{
                    "& .MuiDialog-paper": {
                        maxWidth: "none",
                        width: "980px",
                        borderRadius: "10px",
                    }
                }}
            >
                <DialogTitleWithButton
                    title={t('pages.seller.order.updateTitle')}
                    onClick={handleClickClose}
                />
                <DialogContent sx={{ padding: "10px 40px 45px" }}>
                    <Grid container>
                        <Grid item xs={4}>
                            <Typography variant="h5" color="inherit">№{selectedOrder.id} {t("pages.user.order.from")} {selectedOrder.date}</Typography>
                            <Typography variant="h5" color="inherit" sx={{ mb: "20px" }}>{selectedOrder.orderStatusName}</Typography>
                            {selectedOrder.trackingNumber != "" && <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>{t("validationProps.trackingNumber")}: {selectedOrder.trackingNumber}</Typography>}
                            <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>{selectedOrder.deliveryType}</Typography>
                            <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>
                                {selectedOrder.city} {selectedOrder.department}
                            </Typography>
                            <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>{selectedOrder.consumerFirstName} {selectedOrder.consumerSecondName}</Typography>
                            <Typography variant="subtitle1" color="inherit" sx={{ mb: "20px" }}>{selectedOrder.consumerPhone}</Typography>
                            <Typography variant="subtitle1" color="inherit">{selectedOrder.consumerEmail}</Typography>
                        </Grid>
                        <Grid item xs={8}>
                            {selectedOrder.orderProductsResponse?.map((orderProduct, index) => {
                                return (
                                    <Box key={index} sx={{ display: "flex", justifyContent: "space-between", alignItems: "stretch", mb: "10px" }}>
                                        <Box sx={{ display: "flex" }}>
                                            <Box sx={{ width: 52, height: 52, mr: "10px" }}>
                                                <Img alt={`image `} src={orderProduct.productImage != "" ? orderProduct.productImage : small_empty} />
                                            </Box>
                                            <Box sx={{ display: "flex", alignItems: "center" }}>
                                                <LinkRouter underline="hover" color="inherit" to={`/product/${orderProduct.productUrlSlug}`}>
                                                    <Typography color="inherit" variant="subtitle1" sx={{ width: "200px", mr: "10px" }}>{orderProduct.productName}</Typography>
                                                </LinkRouter>
                                            </Box>
                                        </Box>
                                        <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center", width: "90px" }}>
                                            <Typography color="inherit" variant="subtitle1">{t("pages.user.order.price")}</Typography>
                                            <Typography color="inherit" variant="subtitle1">{orderProduct.price} {t("currency")}</Typography>
                                        </Box>
                                        <Box sx={{ display: "flex", flexDirection: "column", justifyContent: "center" }}>
                                            <Typography color="inherit" variant="subtitle1">{t("pages.user.order.count")}</Typography>
                                            <Typography color="inherit" variant="subtitle1">{orderProduct.count}</Typography>
                                        </Box>
                                    </Box>
                                )
                            })}
                            <Box sx={{ display: "flex", justifyContent: "space-between", mb: "20px" }}>
                                <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "700" }}>{t("pages.user.order.payment")}</Typography>
                                <Typography variant="subtitle1" color="inherit">
                                    {t("pages.ordering.paymentUponReceipt")}
                                </Typography>
                            </Box>
                            <Box sx={{ display: "flex", justifyContent: "space-between", mb: "20px" }}>
                                <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "700" }}>{t("pages.user.order.delivery")}</Typography>
                                <Typography variant="subtitle1" color="inherit">{t("pages.ordering.accordingCarrierTariffs")}</Typography>
                            </Box>
                            <Box sx={{ display: "flex", justifyContent: "space-between" }}>
                                <Typography variant="subtitle1" color="inherit" sx={{ fontWeight: "700" }}>{t("pages.user.order.total")}</Typography>
                                <Typography variant="subtitle1" color="inherit">
                                    {selectedOrder.totalPrice} {t("currency")}
                                </Typography>
                            </Box>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions sx={{ padding: "0 40px 45px", width: "100%" }}>
                    {selectedOrder.canUpdate && <Box sx={{ display: "flex", justifyContent: "space-between", width: "100%" }}>
                        {selectedOrder.orderStatusId == orderStatusIdSent && <Button
                            color="secondary"
                            variant="outlined"
                            sx={{
                                width: "auto",
                                px: "15.5px",
                                py: "8.5px",
                                textTransform: "none",
                                borderRadius: "10px",
                                fontSize: "18px",
                                height: "40px",
                                border: "1px solid #0E7C3A",
                                mt: "10px"
                            }}
                            onClick={async () => {
                                await UpdateOrderForSeller(selectedOrder.id, { orderStatusId: orderStatusIdCompleted, trackingNumber: "" })
                                handleClickClose()
                            }}
                        >
                            {t("pages.user.order.complete")}
                        </Button>}
                        {selectedOrder.orderStatusId == orderStatusIdInProcess && <UpdateTrackingNumber id={selectedOrder.id} afterUpdate={() => handleClickClose()} />}
                        <Button
                            color="primary"
                            variant="outlined"
                            sx={{
                                width: "auto",
                                px: "15.5px",
                                py: "8.5px",
                                textTransform: "none",
                                borderRadius: "10px",
                                fontSize: "18px",
                                height: "40px",
                                border: "1px solid #F45626",
                                mt: "10px",
                                ml: "auto"
                            }}
                            onClick={async () => {
                                await UpdateOrderForSeller(selectedOrder.id, { orderStatusId: orderStatusIdSent, trackingNumber: "" })
                                handleClickClose()
                            }}
                        >
                            {t("pages.user.order.cancel")}
                        </Button>
                    </Box>}
                </DialogActions>
            </Dialog>
        </>
    )
}

export default OrderUpdate;