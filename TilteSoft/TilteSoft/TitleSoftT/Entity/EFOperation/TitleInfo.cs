using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TitleSoftT.Common;
using TitleSoftT.Entity.Model;
using TitleSoftT.Models;
using TitleSoftT.ServiceProxy;

namespace TitleSoftT.Entity.EFOperation
{
    public class TitleInfo : MessageBase
    {
        public bool AddList(List<Model.TitleInfo> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    DbContext.TitleInfo.Add(list[i]);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            DbContext.ChangeTracker.DetectChanges();
            DbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// 返回2数据库已存在该数据 ，返回1成功插入数据 ， 返回101保存数据异常
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Model.TitleInfo model)
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var info = DbContext.TitleInfo.Where(c => c.OldID == model.OldID || c.NewTitle == model.NewTitle).FirstOrDefault();
                    if (info != null && info.ID > 0)
                    {
                        return 2;
                    }
                    else
                    {
                        DbContext.TitleInfo.Add(model);
                        //DbContext.ChangeTracker.DetectChanges();
                        DbContext.SaveChanges();
                        dbContextTransaction.Commit();
                        return 1;
                    }
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return 101;
                }
            }
        }

        #region
        public TitleInfoListResponse SelectTitleInfoList(TitleInfoRequest Request)
        {
            TitleInfoListResponse response = new TitleInfoListResponse();
            try
            {

                using (var entities = new ResKey_dbEntities())
                {
                    var rowCount = Request.PageIndex * Request.PageSize;
                    var pageSize = Request.PageSize;
                    var list = new List<Expression<Func<Entity.Model.TitleInfo, bool>>>();

                    if (Request.ID.HasValue)
                    {
                        list.Add(c => c.ID == Request.ID.Value);
                    }
                    if (Request.IsHandle.HasValue)
                    {
                        list.Add(c => c.IsHandle == Request.IsHandle);
                    }
                    Expression<Func<Entity.Model.TitleInfo, bool>> entitiesQuery = null;
                    foreach (var expression in list)
                    {
                        entitiesQuery = entitiesQuery == null ? expression : entitiesQuery.And(expression);
                    }
                    var dbset = entities.TitleInfo;
                    var query = entitiesQuery == null ? dbset : dbset.Where(entitiesQuery);
                    var mainWindowModel = query.Select(entity => new MainWindowModel
                    {
                        ID = entity.ID,
                        IsAdd = entity.IsAdd,
                        IsHandle = entity.IsHandle,
                        OldID = entity.OldID,
                        ModificationTime = entity.ModificationTime,
                        NewTitle = entity.NewTitle,
                        OldTitle = entity.OldTitle,
                        UpTime = entity.UpTime
                    });
                    var arrayL = mainWindowModel.OrderBy(c => c.ID)
                     .Skip(rowCount)
                     .Take(pageSize)
                     .ToArray()
                     .OrderByDescending(c => c.ID).ToArray();
                    response.mainWindowModels = arrayL;
                    response.RowCount = arrayL.Count();
                    response.PageSize = pageSize;

                }
            }
            catch (Exception exception)
            {
                //response.Error = new Error
                //{
                //    ErrorMessage = exception.GetBaseException().Message,
                //    ErrorCode = 5001
                //};
            }
            return response;
        }
        #endregion

        #region 获取某用户下标签的集合
        /// <summary>
        /// 获取某用户下标签的集合
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public TitleInfoListResponse SelectUserTitleInfoList(TitleInfoRequest Request)
        {
            TitleInfoListResponse response = new TitleInfoListResponse();
            try
            {
                using (var entities = new ResKey_dbEntities())
                {
                    var rowCount = Request.PageIndex * Request.PageSize;
                    var pageSize = Request.PageSize;
                    var list = new List<Expression<Func<Entity.Model.UserInfo, bool>>>();
                    list.Add(c => c.UserNo == Request.UserNo);
                    list.Add(c => c.IsEnable == 1);
                    list.Add(c => c.TitleInfo.IsHandle == 0);
                    if (Request.IsStep.HasValue)
                    {
                        list.Add(c => c.IsStep == Request.IsStep);
                    }
                    else
                    {
                        list.Add(c => c.IsStep != 2);
                    }
                    Expression<Func<Entity.Model.UserInfo, bool>> entitiesQuery = null;
                    foreach (var expression in list)
                    {
                        entitiesQuery = entitiesQuery == null ? expression : entitiesQuery.And(expression);
                    }
                    var dbset = entities.UserInfo;
                    var query = entitiesQuery == null ? dbset : dbset.Where(entitiesQuery);
                    var mainWindowModel = query.Select(entity => new MainWindowModel
                    {
                        ID = entity.TitleInfo.ID,
                        IsAdd = entity.TitleInfo.IsAdd,
                        IsHandle = entity.TitleInfo.IsHandle,
                        OldID = entity.TitleInfo.OldID,
                        ModificationTime = entity.TitleInfo.ModificationTime,
                        NewTitle = entity.TitleInfo.NewTitle,
                        OldTitle = entity.TitleInfo.OldTitle,
                        UpTime = entity.TitleInfo.UpTime
                    });
                    var arrayL = mainWindowModel.OrderBy(c => c.ID)
                     .Skip(rowCount)
                     .Take(pageSize)
                     .ToArray()
                     .OrderByDescending(c => c.ID).ToArray();
                    //int c = a % b == 0 ? a / b : a / b + 1;
                    response.PageMax = (mainWindowModel.Count() % Request.PageSize == 0 ? mainWindowModel.Count() / Request.PageSize : mainWindowModel.Count() / Request.PageSize + 1) - 1;
                    //mainWindowModel.Count()/Request.PageSize;
                    response.mainWindowModels = arrayL;
                    response.RowCount = arrayL.Count();
                    response.PageSize = pageSize;

                }
            }
            catch (Exception exception)
            {
                //response.Error = new Error
                //{
                //    ErrorMessage = exception.GetBaseException().Message,
                //    ErrorCode = 5001
                //};
            }
            return response;
        }
        #endregion

        #region 关键字与用户绑定
        public int TitleAddUser(TitleInfoRequest Request)
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var rowCount = Request.PageIndex * Request.PageSize;
                    var pageSize = Request.PageSize;
                    var list = new List<Expression<Func<Entity.Model.TitleInfo, bool>>>();
                    if (Request.IsHandle.HasValue)
                    {
                        list.Add(c => c.IsHandle == Request.IsHandle);
                    }
                    list.Add(c => c.IsOccupy == 0);
                    Expression<Func<Entity.Model.TitleInfo, bool>> entitiesQuery = null;
                    foreach (var expression in list)
                    {
                        entitiesQuery = entitiesQuery == null ? expression : entitiesQuery.And(expression);
                    }
                    var dbset = DbContext.TitleInfo;
                    var query = entitiesQuery == null ? dbset : dbset.Where(entitiesQuery);
                    var mainWindowModel = query.Select(entity => new MainWindowModel
                    {
                        ID = entity.ID,
                        IsAdd = entity.IsAdd,
                        IsHandle = entity.IsHandle,
                        OldID = entity.OldID,
                        ModificationTime = entity.ModificationTime,
                        NewTitle = entity.NewTitle,
                        OldTitle = entity.OldTitle,
                        UpTime = entity.UpTime
                    });
                    var arrayL = mainWindowModel.OrderBy(c => c.ID)
                     .Skip(rowCount)
                     .Take(pageSize)
                     .ToArray()
                     .OrderByDescending(c => c.ID).ToArray();

                    for (int i = 0; i < arrayL.Count(); i++)
                    {
                        var ID = arrayL[i].ID;
                        var dbModel = DbContext.TitleInfo.Where(c => c.ID == ID).FirstOrDefault();
                        if (dbModel != null)
                        {
                            dbModel.IsOccupy = 1;//已占用
                            var info = DbContext.UserInfo.Where(c => c.TitleInfoID == dbModel.ID && c.IsEnable == 1).FirstOrDefault();
                            if (info != null && info.ID > 0)
                            {
                                //return 2;
                            }
                            else
                            {
                                Model.UserInfo userInfo = new UserInfo
                                {
                                    IsEnable = 1,
                                    TitleInfoID = dbModel.ID,
                                    UserName = Request.UserName,
                                    UserNo = Request.UserNo,
                                };
                                DbContext.UserInfo.Add(userInfo);
                                DbContext.SaveChanges();
                            }

                        }
                    }
                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return 101;
                }
            }
        }
        #endregion

        #region 把百度图片与关键字绑定
        /// <summary>
        /// 把百度图片与关键字绑定
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public int UserBindBaiDuUrl(SavaPicRequest Request)
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var dbset = DbContext.UserInfo.Where(c => c.TitleInfoID == Request.TitleInfoID && c.IsEnable == 1).FirstOrDefault();
                    if (dbset != null && dbset.ID > 0)
                    {
                        dbset.IsStep = 2;
                        DbContext.SaveChanges();
                        for (int i = 0; i < Request.BaiDuUrlList.Count; i++)
                        {
                            Model.BaiDuInfo baiDuInfo = new BaiDuInfo
                            {
                                BaiDuUrl = Request.BaiDuUrlList[i],
                                IsEnable = 1,
                                TitleInfoID = Request.TitleInfoID
                            };
                            DbContext.BaiDuInfo.Add(baiDuInfo);
                            DbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        return 3;
                    }
                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return 101;
                }
            }


        }
        #endregion

        #region 查询标签下百度图片
        /// <summary>
        /// 查询标签下百度图片
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public BaiDuInfoResponse SelectBaiDuInfoList(BaiDuInfoRequest Request)
        {
            BaiDuInfoResponse response = new BaiDuInfoResponse();
            try
            {
                using (var entities = new ResKey_dbEntities())
                {
                    //var list = new List<Expression<Func<Entity.Model.TitleInfo, bool>>>();
                    if (Request.TitleID.HasValue)
                    {
                        //list.Add(c => c.ID == Request.TitleID);
                        //var TitleID = entities.UserInfo.Where(c => c.ID == Request.TitleID).FirstOrDefault().TitleInfoID;
                        var titleInfoModel = entities.TitleInfo.Where(c => c.ID == Request.TitleID).FirstOrDefault();
                        if (titleInfoModel.ID > 0)
                        {
                            response.ID = titleInfoModel.ID;
                            response.IsAdd = titleInfoModel.IsAdd;
                            response.IsHandle = titleInfoModel.IsHandle;
                            response.ModificationTime = titleInfoModel.ModificationTime;
                            response.NewTitle = titleInfoModel.NewTitle;
                            response.OldID = titleInfoModel.OldID;
                            response.OldTitle = titleInfoModel.OldTitle;
                            response.UpTime = titleInfoModel.UpTime;
                            var userInfo = titleInfoModel.UserInfo.FirstOrDefault(c => c.TitleInfoID == Request.TitleID);
                            response.UserName = userInfo?.UserName;
                            response.UserNo = userInfo?.UserNo;
                            List<PictureControlModel> PicModelList = new List<PictureControlModel>();
                            var BaiDuInfoList = titleInfoModel.BaiDuInfo.ToList();
                            for (int i = 0; i < BaiDuInfoList.Count; i++)
                            {
                                PictureControlModel pictureModel = new PictureControlModel();
                                pictureModel.Url = BaiDuInfoList[i].BaiDuUrl;
                                //pictureModel.IsCheck = BaiDuInfoList[i].IsEnable;
                                PicModelList.Add(pictureModel);
                            }
                            response.PicModelList = PicModelList;
                        }
                    }
                    else
                    {
                        //return response;
                    }
                }
            }
            catch (Exception exception)
            {
                //response.Error = new Error
                //{
                //    ErrorMessage = exception.GetBaseException().Message,
                //    ErrorCode = 5001
                //};
            }
            return response;
        }
        #endregion

        #region 隐藏标签
        /// <summary>
        /// 隐藏关键字
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int TitleHandle(TitleHandleRequest request)
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {

                    var dbset = DbContext.TitleInfo.Where(c => c.ID == request.ID).FirstOrDefault();
                    if (dbset != null && dbset.ID > 0)
                    {
                        dbset.IsHandle = 3;
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        return 3;
                    }
                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return 101;
                }
            }

        }


        public int TitleHandle(string TitleName)
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {

                    var dbset = DbContext.TitleInfo.Where(c => c.NewTitle == TitleName).FirstOrDefault();
                    if (dbset != null && dbset.ID > 0)
                    {
                        dbset.IsHandle = 3;
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        return 3;
                    }
                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return 101;
                }
            }

        }

        #endregion



        #region 查询所有关键字
        /// <summary>
        /// 查询所有关键字
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public TitleInfoListResponse SelectAllTitleInfoList(TitleInfoRequest Request)
        {
            TitleInfoListResponse response = new TitleInfoListResponse();
            try
            {
                using (var entities = new ResKey_dbEntities())
                {
                    var rowCount = Request.PageIndex * Request.PageSize;
                    var pageSize = Request.PageSize;
                    var list = new List<Expression<Func<Entity.Model.TitleInfo, bool>>>();
                    list.Add(c => c.IsHandle == 0);
                    if (!string.IsNullOrWhiteSpace(Request.TitleName))
                    {
                        list.Add(c => c.NewTitle.Contains(Request.TitleName));
                    }
                    Expression<Func<Entity.Model.TitleInfo, bool>> entitiesQuery = null;
                    foreach (var expression in list)
                    {
                        entitiesQuery = entitiesQuery == null ? expression : entitiesQuery.And(expression);
                    }
                    var dbset = entities.TitleInfo;
                    var query = entitiesQuery == null ? dbset : dbset.Where(entitiesQuery);
                    var mainWindowModel = query.Select(entity => new MainWindowModel
                    {
                        ID = entity.ID,
                        IsAdd = entity.IsAdd,
                        IsHandle = entity.IsHandle,
                        OldID = entity.OldID,
                        ModificationTime = entity.ModificationTime,
                        NewTitle = entity.NewTitle,
                        OldTitle = entity.OldTitle,
                        UpTime = entity.UpTime
                    });
                    var arrayL = mainWindowModel.OrderBy(c => c.ID)
                     .Skip(rowCount)
                     .Take(pageSize)
                     .ToArray()
                     .OrderByDescending(c => c.ID).ToArray();
                    //int c = a % b == 0 ? a / b : a / b + 1;
                    response.PageMax = (mainWindowModel.Count() % Request.PageSize == 0 ? mainWindowModel.Count() / Request.PageSize : mainWindowModel.Count() / Request.PageSize + 1) - 1;
                    //mainWindowModel.Count()/Request.PageSize;
                    response.mainWindowModels = arrayL;
                    response.RowCount = arrayL.Count();
                    response.PageSize = pageSize;

                }
            }
            catch (Exception exception)
            {

            }
            return response;
        }
        #endregion

        #region 把百度图片与关键字绑定
        /// <summary>
        /// 把百度图片与关键字绑定
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public int UserBindBaiDuUrlT(string TitleName, List<string> urlList)
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var dbset = DbContext.UserInfo.Where(c => c.TitleInfo.NewTitle == TitleName && c.IsEnable == 1).FirstOrDefault();
                    if (dbset != null && dbset.ID > 0)
                    {
                        dbset.IsStep = 2;
                        DbContext.SaveChanges();
                        var TitleID = dbset.TitleInfo.ID;
                        var Time = DateTime.Now.ToString();
                        for (int i = 0; i < urlList.Count; i++)
                        {
                            Model.BaiDuInfo baiDuInfo = new BaiDuInfo
                            {
                                BaiDuUrl = urlList[i],
                                IsEnable = 1,
                                TitleInfoID = TitleID,
                                UpTime = Time
                            };
                            DbContext.BaiDuInfo.Add(baiDuInfo);
                            DbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        return 3;
                    }
                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return 101;
                }
            }
        }
        #endregion

        #region 解除绑定
        public int RelieveBinds(int TitleID)
        {
            using (var dbContextTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {



                    var dbset = DbContext.UserInfo.Where(c => c.TitleInfo.ID == TitleID && c.IsEnable == 1).FirstOrDefault();

                    if (dbset != null && dbset.ID > 0)
                    {


                        var dbList = DbContext.BaiDuInfo.Where(c => c.IsEnable == 1 && c.TitleInfoID == TitleID).ToList();
                        foreach (var item in dbList)
                        {
                            DbContext.BaiDuInfo.Remove(item);
                            DbContext.SaveChanges();
                        }
                        dbset.IsStep = 1;
                        DbContext.SaveChanges();
                        //dbset.IsStep = 2;
                        //DbContext.SaveChanges();
                        //var TitleID = dbset.TitleInfo.ID;
                        //for (int i = 0; i < urlList.Count; i++)
                        //{
                        //    Model.BaiDuInfo baiDuInfo = new BaiDuInfo
                        //    {
                        //        BaiDuUrl = urlList[i],
                        //        IsEnable = 1,
                        //        TitleInfoID = TitleID
                        //    };
                        //    DbContext.BaiDuInfo.Add(baiDuInfo);
                        //    DbContext.SaveChanges();
                        //}
                    }
                    else
                    {
                        return 3;
                    }
                    dbContextTransaction.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    dbContextTransaction.Rollback();
                    return 101;
                }
            }
        }


        #endregion

    }
}
