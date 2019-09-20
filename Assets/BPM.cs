using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour
{  
//    private float next_note_time;
//    private float msec_per_beat;
//    private float ahead;
//    string status;
//
//    interval_span;
//    number;
//    interval_function: number;
//
//    private float before_recorded;
//    private float clear_recorded_timeout;
//    // Start is called before the first frame update
//    void Init(float bpm)
//    {
//        this.next_note_time = 0;
//        this.ahead = 2000;
//
//        this.setBPM(bpm);
//        this.setCallbackFunction(callback);
//
//        this.interval_function = null;
//        this.interval_span = 25;
//
//        this.status = 'stopping';
//    }
//
//    private void Record() {
//        const current_time = Time.time;
//
//        if (before_recorded>= 0) {
//            this.setMSPB(current_time - this.before_recorded);
//        }
//
//        if (this.status === 'stopping') this.resume();
//
//        this.before_recorded = current_time;
//
//        if (this.clear_recorded_timeout) clearTimeout(this.clear_recorded_timeout);
//
//        // @ts-ignore
//        this.clear_recorded_timeout = setTimeout(() => {
//            this.before_recorded = null;
//        }, 2000);
//    }
//
//    setCallbackFunction(func: () => void) {
//        this.callback_function = func;
//    }
//
//    setBPM(bpm) {
//        this.setMSPB(60000/bpm);
//    }
//
//    setMSPB(mspb) {
//        this.msec_per_beat = mspb;
//
//        this.outputBPM();
//
//        this.interval_span = this.msec_per_beat * 0.65;
//    }
//
//    outputBPM() {
//        console.log(`current_bpm : ${60000 / this.msec_per_beat}`);
//
//        this.bpm_meter.textContent = `${60000 / this.msec_per_beat}`;
//    }
//
//    resume() {
//        if (this.status === 'playing') this.stop();
//        this.status = 'playing';
//
//        this.interval_function = setInterval(() => {
//            this.main();
//        }, this.interval_span);
//    }
//
//    stop() {
//        if (this.status === 'playing') {
//            clearInterval(this.interval_function);
//            this.status = 'stopping';
//        }
//    }
//
//    private main() {
//        if (performance.now() > this.next_note_time + this.ahead) {
//            setTimeout(()=> {
//                this.bang();
//            }, );
//
//            this.next_note_time += this.msec_per_beat;
//        }
//    }
//
//    private bang() {
//        this.callback_function();
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        
//    }
}
