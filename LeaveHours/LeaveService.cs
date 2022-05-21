namespace LeaveHours
{
    public class LeaveService
    {
        /// <summary>
        /// 計算一天總請假時數，每天上班時間是 9 - 18 點，12 - 13 是休息時間
        /// 若請假 9 - 18 點，傳回 8 (小時)
        /// 若請假 9 - 17 點，傳回 7 (小時)
        /// 若請假 9 - 12 點，傳回 3 (小時)
        /// 若請假 9 - 13 點，傳回 3 (小時)，因為午休到 13 點
        /// 若請假 12 - 14 點，傳回 1 (小時)，因為午休到 13 點
        /// 若請假 8 -18 點，傳回 8 (小時)，因為 9 點之後才算上班
        /// 若請假 9 -23 點，傳回 8 (小時)，因為 18 點之後就下班了
        /// </summary>
        /// <param name="startHour"></param>
        /// <param name="endHour"></param>
        /// <returns></returns>
        public int CalcTotalLeaveHours(int startHour, int endHour)
        {
            var wrokingTimeTable = WorkingTimeTable();
            var leaveTimeTable = LeaveTimeTable(startHour, endHour);
            return LeaveHoursStat(wrokingTimeTable, leaveTimeTable);
        }

        private int LeaveHoursStat(List<int> workingTimeTable, List<int> leaveTimeTable)
        {
            return workingTimeTable.Intersect(leaveTimeTable).Count();
        }

        private List<int> LeaveTimeTable(int startHour, int endHour)
        {
            int count = endHour - startHour;
            return TimeTable(startHour, count);
        }

        private List<int> WorkingTimeTable()
        {
            int workingStartHour = 9;
            int count = 9;
            return TimeTable(workingStartHour, count);
        }

        /// <summary>
        /// 每天上班時間是 9 - 18 點，12 - 13 是休息時間
        /// </summary>
        /// <returns></returns>
        private List<int> TimeTable(int startHour, int count)
        {
            var timeTable = Enumerable.Range(startHour, count).ToList();

            int restTime = 12;
            timeTable.Remove(restTime);

            return timeTable;
        }

    }
}
