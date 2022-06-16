export interface IDialog {
    open: boolean,
    handleClickClose: React.MouseEventHandler<HTMLButtonElement> | undefined,
    button: any,

    formik: any,
    isSubmitting: boolean,
    handleSubmit: (e?: React.FormEvent<HTMLFormElement> | undefined) => void

    dialogTitle: string,
    dialogBtnCancel: string,
    dialogBtnConfirm: string,

    dialogContent: any,
};