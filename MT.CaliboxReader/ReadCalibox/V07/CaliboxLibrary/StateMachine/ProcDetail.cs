using CaliboxLibrary.StateMachine;
using System;
using System.Collections.Generic;

namespace CaliboxLibrary
{
    public enum ProcessState
    {
        Idle,
        TimeOut,
        WaitBefore,
        Send,
        WaitAfter,
        //Attends,
        //Next,
        Exit,
        Aborted
    }
    public enum Command
    {
        Begin,
        End,
        Abort,
        Terminate
    }

    public class ProcStateMachine
    {
        private class StateTransition : IEquatable<StateTransition>
        {
            public readonly ProcessState CurrentState;
            public readonly Command Command;
            public StateTransition(ProcessState currentState, Command command)
            {
                CurrentState = currentState;
                Command = command;
                _HashCode = CreateHashCode();
            }
            public override string ToString()
            {
                return $"{CurrentState}/{Command}";
            }

            private int _HashCode = 0;
            private int CreateHashCode()
            {
                var hash = new HashSet<object>
                {
                    CurrentState,
                    Command
                };
                var value = hash.GetHashCode();
                return value;
            }
            public override int GetHashCode()
            {
                return _HashCode;
            }
            //public override bool Equals(object obj)
            //{
            //    var other = obj as StateTransition;
            //    return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
            //}

            public bool Equals(StateTransition other)
            {
                return other != null && GetHashCode() == other.GetHashCode();
            }

        }

        private static Dictionary<StateTransition, ProcessState> _Transitions;
        public ProcessState CurrentState { get; private set; }
        public Command Command { get; private set; }

        public void SetState(ProcessState state)
        {
            CurrentState = state;
            Command = Command.Begin;
        }
        public ProcStateMachine()
        {
            CurrentState = ProcessState.Idle;
            if (_Transitions != null)
            {
                return;
            }
            _Transitions = new Dictionary<StateTransition, ProcessState>
            {
                { new StateTransition(ProcessState.Idle, Command.Begin), ProcessState.WaitBefore },
                { new StateTransition(ProcessState.Idle, Command.Terminate), ProcessState.Idle },
                { new StateTransition(ProcessState.Idle, Command.Abort), ProcessState.Aborted },
                { new StateTransition(ProcessState.Idle, Command.End), ProcessState.Idle },
                { new StateTransition(ProcessState.Exit, Command.Begin), ProcessState.Idle },
                { new StateTransition(ProcessState.Exit, Command.Terminate), ProcessState.Idle },
                { new StateTransition(ProcessState.Exit, Command.Abort), ProcessState.Aborted },
                { new StateTransition(ProcessState.Exit, Command.End), ProcessState.Idle },
                { new StateTransition(ProcessState.TimeOut, Command.Begin), ProcessState.WaitBefore },
                { new StateTransition(ProcessState.TimeOut, Command.End), ProcessState.Exit },
                { new StateTransition(ProcessState.TimeOut, Command.Terminate), ProcessState.Idle },
                { new StateTransition(ProcessState.TimeOut, Command.Abort), ProcessState.Aborted },
                { new StateTransition(ProcessState.WaitBefore, Command.Begin), ProcessState.WaitBefore },
                { new StateTransition(ProcessState.WaitBefore, Command.End), ProcessState.Send },
                { new StateTransition(ProcessState.WaitBefore, Command.Terminate), ProcessState.Idle },
                { new StateTransition(ProcessState.WaitBefore, Command.Abort), ProcessState.Aborted },
                { new StateTransition(ProcessState.Send, Command.Begin), ProcessState.WaitAfter },
                { new StateTransition(ProcessState.Send, Command.End), ProcessState.Idle },
                { new StateTransition(ProcessState.Send, Command.Terminate), ProcessState.Idle },
                { new StateTransition(ProcessState.Send, Command.Abort), ProcessState.Aborted },
                { new StateTransition(ProcessState.WaitAfter, Command.Begin), ProcessState.Exit },
                { new StateTransition(ProcessState.WaitAfter, Command.End), ProcessState.Exit },
                { new StateTransition(ProcessState.WaitAfter, Command.Terminate), ProcessState.Exit },
                { new StateTransition(ProcessState.WaitAfter, Command.Abort), ProcessState.Aborted },
                { new StateTransition(ProcessState.Aborted, Command.Begin), ProcessState.Idle },
                { new StateTransition(ProcessState.Aborted, Command.Terminate), ProcessState.Idle },
                { new StateTransition(ProcessState.Aborted, Command.End), ProcessState.Idle },
                { new StateTransition(ProcessState.Aborted, Command.Abort), ProcessState.Idle },
                //{ new StateTransition(ProcessState.Attends, Command.Begin), ProcessState.Attends },
                //{ new StateTransition(ProcessState.Attends, Command.Terminate), ProcessState.Idle },
                //{ new StateTransition(ProcessState.Attends, Command.End), ProcessState.WaitAfter },
                //{ new StateTransition(ProcessState.Attends, Command.Abort), ProcessState.Aborted },
            };
        }
        public ProcessState GetNext(Command command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            Command = command;
            ProcessState nextState;
            if (_Transitions.Count == 0)
            {

            }
            if (!_Transitions.TryGetValue(transition, out nextState))
            {
                throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
            }
            return nextState;
        }

        public ProcessState MoveNext(Command command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }
    }

    public class ProcDetail
    {
        public ProcStateMachine StateMachine { get; private set; }
        public ProcDetail(gProcMain process)
        {
            StateMachine = new ProcStateMachine();
            ProcName = process;
        }

        public ProcDetail(gProcMain process, OpCode send) : this(process)
        {
            Cmd(send);
        }
        public ProcDetail(gProcMain process, OpCode send, OpCode response) : this(process)
        {
            Cmd(send, response: response);
        }

        public override string ToString()
        {
            return ProcName.ToString();
        }
        public bool IsCommand { get; set; }

        /// <summary>
        /// Wait the total time, else wait for response
        /// </summary>
        public bool WaitToEnd { get; set; }
        public Commands CMD { get; set; } = new Commands();
        public bool AnswerReceived { get; set; }
        public bool Executed { get; set; }
        public void SetAnswer(bool received)
        {
            if (AnswerReceived == received)
            {
                return;
            }
            AnswerReceived = received;
            WaitStartTime = DateTime.Now;
            StateMachine.SetState(ProcessState.WaitAfter);
        }
        public void Cmd(OpCode send, string add = null, OpCode response = OpCode.init)
        {
            IsCommand = true;
            CMD = new Commands(send, add, response);
        }

        public gProcMain ProcName { get; set; }
        public ProcState ProcState { get; set; } = ProcState.Idle;

        public ProcessState ProcStateMachine { get { return StateMachine.CurrentState; } }
        public DateTime ProcStartTime { get; set; }

        //#region Execution Order
        ///**********************************************************
        //* FUNCTION:     Execution Order
        //* DESCRIPTION:  
        //***********************************************************/
        //public static Dictionary<int, ProcessState> ExecutionOrder { get; private set; } = new Dictionary<int, ProcessState>()
        //{
        //    {0, ProcessState.Idle },
        //    {1, ProcessState.TimeOut },
        //    {2, ProcessState.WaitBefore },
        //    {3, ProcessState.Send },
        //    {4, ProcessState.WaitAfter },
        //    //{5, ProcessState.Attends }
        //};
        //public int ExectionIndex { get; set; } = 0;
        //public ProcessState ExecuteionState
        //{
        //    get
        //    {
        //        return ExecutionOrder[ExectionIndex];
        //    }
        //}
        //#endregion

        #region TimeOut
        /**********************************************************
        * FUNCTION:     TimeOut
        * DESCRIPTION:  
        ***********************************************************/
        public DateTime TimeOutStartTime { get; set; }
        public bool TimeOutActive { get; set; }
        public int TimeOutDuration { get; set; }

        private TimeSpan GetElapsedTime(DateTime timeStart)
        {
            return DateTime.Now - timeStart;
        }

        private double GetElapsedTimeMilisecond(DateTime timeStart)
        {
            return GetElapsedTime(timeStart).TotalMilliseconds;
        }

        private bool GetElapsedTimeExided(DateTime timeStart, int timeOutDuration)
        {
            return GetElapsedTimeMilisecond(timeStart) > timeOutDuration;
        }
        public bool TimeOutExeeded()
        {
            if (TimeOutActive)
            {
                if (TimeOutStartTime == DateTime.MinValue)
                {
                    TimeOutStartTime = DateTime.Now;
                    return true;
                }
                if (GetElapsedTimeExided(TimeOutStartTime, TimeOutDuration))
                {
                    ProcState = ProcState.Aborted;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Wait
        /**********************************************************
        * FUNCTION:     Wait
        * DESCRIPTION:  
        ***********************************************************/
        public DateTime WaitStartTime { get; set; }

        public TimeSpan WaitElapsed { get { return GetElapsedTime(WaitStartTime); } }
        public bool WaitBeforeExecuted { get; set; }
        public bool WaitBeforeActive { get; set; }
        public int WaitBeforeDuration { get; set; }
        public bool WaitBeforeExeeded()
        {
            if (!WaitBeforeActive) { return true; }
            return GetWaitStatus(WaitBeforeDuration);
        }
        public bool WaitAfterActive { get; set; }
        public bool WaitAfterExecuted { get; set; }
        public int WaitAfterDuration { get; set; }
        public bool WaitAfterExeeded()
        {
            if (!WaitAfterActive || Attends == 0) { return true; }
            if (WaitToEnd)
            {
                return GetWaitStatus(WaitAfterDuration);
            }
            if (!AnswerReceived)
            {
                return GetWaitStatus(WaitAfterDuration);
            }
            return true;
        }

        private bool GetWaitStatus(int duration)
        {
            if (GetElapsedTimeExided(WaitStartTime, duration))
            {
                ProcState = Executed ? ProcState.Finished : ProcState.Running;
                return true;
            }
            ProcState = ProcState.Waiting;
            return false;
        }
        public bool WaitExeeded()
        {
            switch (StateMachine.CurrentState)
            {
                case ProcessState.WaitBefore:
                    return WaitBeforeExeeded();
                case ProcessState.WaitAfter:
                    return WaitAfterExeeded();
                default:
                    return true;
            }
            //return WaitBeforeExeeded() && WaitAfterExeeded();
        }
        #endregion

        #region Attends
        /**********************************************************
        * FUNCTION:     Attends
        * DESCRIPTION:  
        ***********************************************************/
        public bool AttendsActive { get; set; }
        public int AttendsMax { get; set; }
        public int Attends { get; private set; }
        public bool AttendsExeeded()
        {
            if (AttendsActive)
            {
                if (Attends > AttendsMax)
                {
                    ProcState = ProcState.Aborted;
                    return true;
                }
            }
            return false;
        }
        public void AttendsIncreese()
        {
            Attends++;
            WaitStartTime = DateTime.Now;
            ProcState = ProcState.Waiting;
            StateMachine.SetState(ProcessState.WaitAfter);
        }
        #endregion

        public bool Exeeded()
        {
            return CheckProcesses(out _);
            //if (TimeOutExeeded())
            //{
            //    return true;
            //}
            //if (WaitExeeded())
            //{
            //    return true;
            //}
            //return AttendsExeeded();
        }
        public void Start()
        {
            Reset();
            StateMachine.MoveNext(Command.Begin);
            ProcStartTime = DateTime.Now;
            WaitStartTime = DateTime.Now;
            TimeOutStartTime = ProcStartTime;
            ProcState = ProcState.Running;
            //ProcState = WaitAfterActive || WaitBeforeActive ? ProcState.Waiting : ProcState.Running;
        }
        public void Reset()
        {
            AnswerReceived = false;
            Executed = false;
            Attends = 0;
            StateMachine.SetState(ProcessState.Idle);
            ProcState = ProcState.Idle;
            TimeOutStartTime = DateTime.MinValue;
            ProcStartTime = DateTime.MinValue;
            WaitStartTime = DateTime.MinValue;
        }
        public void Terminate()
        {
            if (ProcState != ProcState.Aborted)
            {
                ProcState = ProcState.Terminated;
            }
            StateMachine.MoveNext(Command.Terminate);
        }

        public bool CheckProcesses(out ProcessState proc)
        {
            var procInit = StateMachine.CurrentState;
            var result = GetNext();
            proc = StateMachine.CurrentState;
            if (TimeOutExeeded())
            {
                if (proc != ProcessState.Aborted)
                {
                    StateMachine.MoveNext(Command.Abort);
                }
                result = true;
            }
            if (procInit != proc)
            {
                Processes.OnChanged(this);
            }
            return result;
        }

        private bool GetNext()
        {
            var proc = StateMachine.CurrentState;
            bool result = false;
            switch (proc)
            {
                case ProcessState.TimeOut:
                    if (!TimeOutActive)
                    {
                        StateMachine.MoveNext(Command.End);
                        return GetNext();
                    }
                    break;
                case ProcessState.WaitBefore:
                    if (!WaitBeforeActive)
                    {
                        StateMachine.MoveNext(Command.End);
                        return GetNext();
                    }
                    if (WaitBeforeExeeded())
                    {
                        WaitBeforeExecuted = true;
                        StateMachine.MoveNext(Command.End);
                        return GetNext();
                    }
                    break;
                case ProcessState.Send:
                    if (!IsCommand)
                    {
                        StateMachine.MoveNext(Command.End);
                        return GetNext();
                    }
                    break;
                case ProcessState.WaitAfter:
                    if (!WaitAfterActive)
                    {
                        StateMachine.MoveNext(Command.End);
                        if (Executed)
                        {
                            ProcState = ProcState.Finished;
                            return true;
                        }
                        return GetNext();
                    }
                    if (WaitAfterExeeded())
                    {
                        WaitAfterExecuted = true;
                        StateMachine.MoveNext(Command.End);
                        if (Executed)
                        {
                            ProcState = ProcState.Finished;
                            return true;
                        }
                        return GetNext();
                    }
                    if (AttendsExeeded())
                    {
                        StateMachine.MoveNext(Command.Abort);
                        return GetNext();
                    }
                    break;
                //case ProcessState.Attends:
                //    if (!AttendsActive)
                //    {
                //        StateMachine.MoveNext(Command.End);
                //        return GetNext();
                //    }
                //    if (AttendsExeeded())
                //    {
                //        StateMachine.MoveNext(Command.Abort);
                //        return GetNext();
                //    }
                //    Attends++;
                //    StateMachine.MoveNext(Command.Begin);
                //    return GetNext();
                case ProcessState.Aborted:
                    break;
                case ProcessState.Exit:
                    if (Executed)
                    {
                        ProcState = ProcState.Finished;
                    }
                    break;
            }
            return result;
        }
    }

    public struct Commands
    {
        public Commands(OpCode send, string add = null, OpCode response = OpCode.init)
        {
            IsCommand = true;
            Send = send;
            SendAdd = add;
            if (response == OpCode.init)
            {
                var rsp = send.ToString().ToLower();
                Enum.TryParse(rsp, out Response);
            }
            else
            {
                Response = response;
            }
        }
        public readonly bool IsCommand;
        public readonly OpCode Send;
        public readonly string SendAdd;
        public readonly OpCode Response;
        public bool CheckResponse(OpCode response)
        {
            if (IsCommand)
            {
                return Response == response;
            }
            return false;
        }
    }

    public class ProcTimes
    {
        public bool Active { get; set; }
        public int Duration { get; set; }
        public DateTime TimeStarted { get; set; }

        public TimeSpan GetElapsedTime()
        {
            return DateTime.Now - TimeStarted;
        }
        public bool ElapsedTimeExceeded()
        {
            return !Active || Duration > GetElapsedTime().TotalMilliseconds;
        }
        public void Clear()
        {
            TimeStarted = DateTime.MinValue;
        }
    }

    public struct Attents
    {
        public bool Active;
        public int Max;
        public int Count;
        public void Clear()
        {
            Count = 0;
        }
        public bool CountExceeded()
        {
            if (!Active) { return true; }
            return Count > Max;
        }

        public bool Increese()
        {
            Count++;
            return CountExceeded();
        }
    }
}