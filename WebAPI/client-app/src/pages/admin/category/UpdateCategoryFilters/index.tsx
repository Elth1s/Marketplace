import Grid from "@mui/material/Grid";
import { Edit } from "@mui/icons-material";

import { FC, useState } from "react";
import { useParams } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { ServerError, UpdateProps } from "../../../../store/types";

import { toLowerFirstLetter } from "../../../../http_comon";
import AdminSellerDialog from "../../../../components/Dialog";
import DialogTitleWithButton from "../../../../components/Dialog/DialogTitleWithButton";
import { AdminSellerDialogActionsStyle, AdminSellerDialogContentStyle } from "../../../../components/Dialog/styled";
import { AdminDialogButton } from "../../../../components/Button/style";

const UpdateCategoryFilters: FC<UpdateProps> = ({ id, afterUpdate }) => {
    const { t } = useTranslation();

    const { GetCategoryFiltersById, GetFilterValues, UpdateCategoryFilters } = useActions();
    const { selectedCategoryFilters } = useTypedSelector((store) => store.category);
    const { filterValues } = useTypedSelector((store) => store.filterValue);

    const [open, setOpen] = useState(false);

    const handleClickOpen = async () => {
        try {
            setOpen(true);
            await GetFilterValues();
            await GetCategoryFiltersById(id);
        } catch (ex) {
        }
    };

    const handleClickClose = () => {
        setOpen(false);
    };

    const update = async () => {
        try {
            await UpdateCategoryFilters(id, selectedCategoryFilters);
            afterUpdate();
            handleClickClose();
        }
        catch (ex) {
        }
    }


    return (
        <>
            <Edit onClick={() => handleClickOpen()} />
            <AdminSellerDialog
                open={open}
                onClose={handleClickClose}
                dialogContent={
                    <>
                        <DialogTitleWithButton
                            title={t('pages.admin.category.updateFiltersTitle')}
                            onClick={handleClickClose}
                        />
                        <AdminSellerDialogContentStyle>
                            <Grid container spacing={2}>
                                <Grid item xs={6}>

                                </Grid>
                                <Grid item xs={6}>

                                </Grid>
                            </Grid>
                        </AdminSellerDialogContentStyle>
                        <AdminSellerDialogActionsStyle>
                            <AdminDialogButton
                                type="submit"
                                variant="contained"
                                color="primary"
                                onClick={update}
                            >
                                {t('pages.admin.main.btnUpdate')}
                            </AdminDialogButton>
                        </AdminSellerDialogActionsStyle>
                    </>
                }
            />
        </>
    )
}

export default UpdateCategoryFilters;