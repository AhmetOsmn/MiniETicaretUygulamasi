export const authController = {
  controllerName: 'auth',
  actions: {
    login: 'Login',
    refreshTokenLogin: 'RefreshTokenLogin',
    googleLogin: 'google-login',
    facebookLogin: 'facebook-login',
    passwordReset: 'password-reset',
    verifyResetToken: 'verify-reset-token',
  },
};

export const roleController = {
  controllerName: 'roles',
  actions: {
    getRoles: '',
    getRoleById: '',
    create: '',
    update: '',
    delete: '',
  },
};

export const basketController = {
  controllerName: 'baskets',
  actions: {
    getBasketItems: '',
    addItemToBasket: '',
    updateQuantity: '',
    removeBasketItem: '',
  },
};

export const applicationServicesController = {
  controllerName: 'applicationServices',
  actions: {
    getAuthorizeDefinitionEndpoints: '',
  },
};

export const filesController = {
  controllerName: 'files',
  actions: {
    getBaseStorageUrl: 'GetBaseStorageUrl',
  },
};

export const productsController = {
  controllerName: 'products',
  actions: {
    get: '',
    getById: '',
    post: '',
    put: '',
    delete: '',
    upload: 'Upload',
    getProductImages: 'GetProductImages',
    deleteProductImage: 'DeleteProductImage',
    changeShowcase: 'ChangeShowcase',
    generateQrCode: 'GenerateQrCode',
    updateStockWithQrCode: 'UpdateStockWithQrCode',
  },
};

export const usersController = {
  controllerName: 'users',
  actions: {
    createUser: '',
    updatePassword: 'update-password',
    assignRoleToUser: 'assign-role-to-user',
    getRolesToUser: 'get-roles-to-user',
  },
};

export const ordersController = {
  controllerName: 'orders',
  actions: {
    createOrder: '',
    completeOrder: 'complete-order',
  },
};

export const authorizationEndpointsController = {
  controllerName: 'authorizationEndpoints',
  actions: {
    AssignRole: '',
    GetRolesToEndpoint: 'get-roles-to-endpoint',
  },
};
